using Polly.Retry;
using RocketManagement.Infrastructure.Serializers.TcpSerializer;
using RocketManagement.Infrastructure.Serializers.TcpSerializer.TelemeterySerializer;
using System.Net;
using System.Net.Sockets;

namespace RocketManagement.Infrastructure.TcpListeners
{
    public class TelemetryTcpSocketListener : ITcpSocketListener
    {
        private string ipAddress;
        private int port;
        private CancellationToken cancellationToken;
        private readonly ITcpSerializer<Telemetry> serializer;
        private readonly AsyncRetryPolicy retryPolicy;

        public TelemetryTcpSocketListener(ITcpSerializer<Telemetry> serializer, AsyncRetryPolicy retryPolicy)
        {
            this.serializer = serializer;
            this.retryPolicy = retryPolicy;
        }

        public string IpAddress => ipAddress;
        public int Port => port;

        public event ITcpSocketListener.DataRecevivedEvent OnDataReceived;

        public async Task Listen(string ipAddress, int port, CancellationToken cancellationToken)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            this.cancellationToken = cancellationToken;

            IPHostEntry ipHost = Dns.GetHostEntry(ipAddress);

            IPAddress ipAddr = ipHost.AddressList[0];

            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                await sender.ConnectAsync(localEndPoint);

                while (!cancellationToken.IsCancellationRequested)
                {
                    byte[] receivedMessage = new byte[36];

                    if (sender.Receive(receivedMessage) > 0)
                    {
                        var obj = serializer.Serialize<Telemetry>(receivedMessage);

                        OnDataReceived?.Invoke(obj);
                    }
                    else
                    {
                        await retryPolicy.ExecuteAsync(async () =>
                         {
                             if (sender.Connected)
                             {
                                 sender.Disconnect(true);
                             }

                             await sender.ConnectAsync(localEndPoint);
                         });
                    }
                }
            }
            catch (Exception exc)
            {
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
        }
    }
}
