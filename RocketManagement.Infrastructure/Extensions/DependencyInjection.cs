using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using RocketManagement.Infrastructure.Configurations;
using RocketManagement.Infrastructure.HttpClients;
using RocketManagement.Infrastructure.IntegrationServices;
using RocketManagement.Infrastructure.Interfaces;
using RocketManagement.Infrastructure.Serializers.TcpSerializer;
using RocketManagement.Infrastructure.Serializers.TcpSerializer.TelemeterySerializer;
using RocketManagement.Infrastructure.TcpListeners;
using System.Net;
using System.Net.Sockets;

namespace RocketManagement.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddCustomRetryPolicy(services);
            AddSerializers(services);
            AddListeners(services);
            AddIntegrationServices(services);
        }
        private static void AddCustomRetryPolicy(this IServiceCollection services)
        {
            services.AddSingleton<IAsyncPolicy<HttpResponseMessage>>(Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.ServiceUnavailable)
                .Or<TimeoutException>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(2)));

            services.AddSingleton<AsyncRetryPolicy>(Policy.Handle<SocketException>().WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(2)));
        }

        private static void AddSerializers(this IServiceCollection services)
        {
            services.AddSingleton<ITcpSerializer<Telemetry>, TelemetrySerializer>();
        }

        private static void AddListeners(this IServiceCollection services)
        {
            services.AddTransient<ITcpSocketListener, TelemetryTcpSocketListener>();
        }

        private static void AddIntegrationServices(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddSingleton<IRocketService, RocketService>();
        }
    }
}
