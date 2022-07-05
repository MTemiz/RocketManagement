using Microsoft.AspNetCore.SignalR;

namespace RocketManagement.BackgroundServices.Hubs
{
    public class TelemetryMessageHub : Hub<ITelemetryMessageClient>
    {
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
