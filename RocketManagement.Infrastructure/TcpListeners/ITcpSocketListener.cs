namespace RocketManagement.Infrastructure.TcpListeners
{
    public interface ITcpSocketListener
    {
        public string IpAddress { get; }
        public int Port { get; }
        delegate void DataRecevivedEvent(object data);
        event DataRecevivedEvent OnDataReceived;
        Task Listen(string ipAddress, int port, CancellationToken cancellationToken);
    }
}
