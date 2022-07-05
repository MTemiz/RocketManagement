using Polly;
using RocketManagement.Infrastructure.HttpClients;
using RocketManagement.Infrastructure.IntegrationServices.Responses;
using RocketManagement.Infrastructure.Interfaces;

namespace RocketManagement.Infrastructure.IntegrationServices
{
    public class RocketService : BaseIntegrationService, IRocketService
    {
        private readonly ServiceHttpClient serviceHttpClient;


        public RocketService(ServiceHttpClient serviceHttpClient, IAsyncPolicy<HttpResponseMessage> retryPolicy) : base(retryPolicy)
        {
            this.serviceHttpClient = serviceHttpClient;
        }

        public async Task<IntegrationServiceResponse> CancelLaunchAsync(string id)
        {
            return await CallService(async () => { return await serviceHttpClient.Client.DeleteAsync($"/rocket/{id}/status/launched"); });
        }

        public async Task<IntegrationServiceResponse> DeployAsync(string id)
        {
            return await CallService(async () => { return await serviceHttpClient.Client.PutAsync($"/rocket/{id}/status/deployed", null); });
        }

        public async Task<IntegrationServiceResponse> GetAllRocketsAsync()
        {
            return await CallService(async () => { return await serviceHttpClient.Client.GetAsync("/rockets"); });
        }

        public async Task<IntegrationServiceResponse> LaunchAsync(string id)
        {
            return await CallService(async () => { return await serviceHttpClient.Client.PutAsync($"/rocket/{id}/status/launched", null); });
        }
    }
}
