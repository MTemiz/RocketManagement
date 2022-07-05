using RocketManagement.UI.BackgroundJobs;

namespace RocketManagement.UI.Hubs
{
    public interface ITelemetryMessageClient
    {
        Task TelemetryReceived(string rocketSystemId, float altitude, float speed, float acceleration, float thrust, float temperature);

    }
}