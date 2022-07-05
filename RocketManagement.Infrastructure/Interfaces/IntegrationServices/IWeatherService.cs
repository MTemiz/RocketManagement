using RocketManagement.Infrastructure.IntegrationServices.Responses;

namespace RocketManagement.Infrastructure.Interfaces
{
    public interface IWeatherService
    {
        Task<IntegrationServiceResponse> GetWeatherAsync();
    }
}
