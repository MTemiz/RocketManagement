using Microsoft.AspNetCore.SignalR;
using RocketManagement.Application.Interfaces.IntegrationServices;
using RocketManagement.BackgroundServices.Hubs;
using RocketManagement.Infrastructure.Listeners;
using RocketManagement.Infrastructure.Serializers;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace RocketManagement.BackgroundServices.BackgroundJobs
{
    public class TelemetryBackgroundJob : BackgroundService
    {
        private readonly IHubContext<TelemetryMessageHub, ITelemetryMessageClient> hubContext;
        private readonly IRocketService rocketService;

        public TelemetryBackgroundJob(IHubContext<TelemetryMessageHub, ITelemetryMessageClient> hubContext, IRocketService rocketService)
        {
            this.hubContext = hubContext;
            this.rocketService = rocketService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rocketList = await rocketService.GetAllRocketsAsync();

            foreach (var rocket in rocketList.Response)
            {
                Task workerTask = await Task.Factory.StartNew(async () =>
                {
                    var telemetryTcpSocketListener = new TelemetryTcpSocketListener();

                    telemetryTcpSocketListener.OnDataReceived += Consumer_OnTelemetryReceived;

                    await telemetryTcpSocketListener.Listen("host.docker.internal", rocket.Telemetry.Port, stoppingToken);
                });

            }
        }

        private void Consumer_OnTelemetryReceived(object obj)
        {
            var telemetry = (Telemetry)obj;

            hubContext.Clients.Group(telemetry.rocketSystemId).TelemetryReceived(telemetry.rocketSystemId, telemetry.altitude, telemetry.speed, telemetry.acceleration, telemetry.thrust, telemetry.temperature);
        }
    }
}
