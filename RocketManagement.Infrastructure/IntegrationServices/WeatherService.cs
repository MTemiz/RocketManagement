using Polly;
using RocketManagement.Infrastructure.HttpClients;
using RocketManagement.Infrastructure.IntegrationServices.Responses;
using RocketManagement.Infrastructure.Interfaces;

namespace RocketManagement.Infrastructure.IntegrationServices
{
    public class WeatherService : BaseIntegrationService, IWeatherService
    {
        private readonly ServiceHttpClient serviceHttpClient;

        public WeatherService(ServiceHttpClient serviceHttpClient, IAsyncPolicy<HttpResponseMessage> retryPolicy) : base(retryPolicy)
        {
            this.serviceHttpClient = serviceHttpClient;
        }

        public async Task<IntegrationServiceResponse> GetWeatherAsync()
        {
            return await CallService(async () => { return await serviceHttpClient.Client.GetAsync("/weather"); });
        }
    }
}
