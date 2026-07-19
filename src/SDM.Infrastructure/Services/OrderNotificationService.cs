using Microsoft.AspNetCore.SignalR;
using SDM.Application.Interfaces;
using SDM.Infrastructure.Hubs;

namespace SDM.Infrastructure.Services;

/// <summary>
/// Infrastructure implementation of <see cref="IOrderNotificationService"/>.
/// Broadcasts the <c>OrderCreated</c> SignalR event to all connected admin clients
/// via <see cref="NotificationHub"/>.
/// </summary>
internal sealed class OrderNotificationService : IOrderNotificationService
{
    private readonly IHubContext<NotificationHub> _hub;

    /// <summary>Initializes a new instance of <see cref="OrderNotificationService"/>.</summary>
    public OrderNotificationService(IHubContext<NotificationHub> hub) => _hub = hub;

    /// <inheritdoc/>
    public async Task NotifyOrderCreatedAsync(
        Guid orderId,
        string customerName,
        DateTime createdAt,
        int itemCount,
        CancellationToken cancellationToken = default)
    {
        await _hub.Clients.All.SendAsync(
            "OrderCreated",
            new
            {
                orderId,
                customerName,
                createdAt,
                itemCount
            },
            cancellationToken);
    }
}
