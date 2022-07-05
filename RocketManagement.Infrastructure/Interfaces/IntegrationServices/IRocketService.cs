using RocketManagement.Infrastructure.IntegrationServices.Responses;

namespace RocketManagement.Infrastructure.Interfaces
{
    public interface IRocketService
    {
        Task<IntegrationServiceResponse> GetAllRocketsAsync();
        Task<IntegrationServiceResponse> LaunchAsync(string id);
        Task<IntegrationServiceResponse> DeployAsync(string id);
        Task<IntegrationServiceResponse> CancelLaunchAsync(string id);
    }
}
