using Microsoft.AspNetCore.SignalR;

namespace SDM.Infrastructure.Hubs;

/// <summary>
/// SignalR hub for real-time notifications.
/// Implementation will be added in later sprints.
/// </summary>
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
