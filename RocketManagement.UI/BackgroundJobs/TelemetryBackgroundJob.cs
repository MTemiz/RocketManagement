using MediatR;
using Microsoft.AspNetCore.SignalR;
using Polly.Retry;
using RocketManagement.Application.Features;
using RocketManagement.Application.Features.Rocket.Dtos;
using RocketManagement.Application.Features.Rocket.Queries;
using RocketManagement.Infrastructure.Interfaces;
using RocketManagement.Infrastructure.Serializers.TcpSerializer;
using RocketManagement.Infrastructure.Serializers.TcpSerializer.TelemeterySerializer;
using RocketManagement.Infrastructure.TcpListeners;
using RocketManagement.UI.Hubs;

namespace RocketManagement.UI.BackgroundJobs
{
    public class TelemetryBackgroundJob : BackgroundService
    {
        private readonly IHubContext<TelemetryMessageHub, ITelemetryMessageClient> hubContext;
        private readonly ITcpSerializer<Telemetry> serializer;
        private readonly AsyncRetryPolicy retryPolicy;
        private readonly IServiceProvider services;

        public TelemetryBackgroundJob(IHubContext<TelemetryMessageHub, ITelemetryMessageClient> hubContext,
            ITcpSerializer<Telemetry> serializer, AsyncRetryPolicy retryPolicy, IServiceProvider services)
        {
            this.hubContext = hubContext;
            this.serializer = serializer;
            this.retryPolicy = retryPolicy;
            this.services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GenericResponse<List<RocketDto>> result = new();

            using (var scope = services.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                result = await mediator.Send(new GetAllRocketsQuery());
            }

            if (!result.IsSuccess)
            {
                throw new ArgumentNullException(nameof(result));
            }
            foreach (var rocket in result.Model)
            {
                Task workerTask = await Task.Factory.StartNew(async () =>
                {
                    var telemetryTcpSocketListener = new TelemetryTcpSocketListener(serializer, retryPolicy);

                    telemetryTcpSocketListener.OnDataReceived += Consumer_OnTelemetryReceived;

                    await telemetryTcpSocketListener.Listen("host.docker.internal", rocket.Telemetry.Port, stoppingToken);
                }, TaskCreationOptions.LongRunning);
            }
        }

        private void Consumer_OnTelemetryReceived(object obj)
        {
            var telemetry = (Telemetry)obj;

            hubContext.Clients.Group(telemetry.rocketSystemId).TelemetryReceived(telemetry.rocketSystemId, telemetry.altitude, telemetry.speed, telemetry.acceleration, telemetry.thrust, telemetry.temperature);
        }
    }
}
