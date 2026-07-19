namespace SDM.Application.Interfaces;

/// <summary>
/// Abstraction for sending real-time order events to connected admin clients.
/// Implemented in Infrastructure using SignalR IHubContext&lt;NotificationHub&gt;.
/// </summary>
public interface IOrderNotificationService
{
    /// <summary>
    /// Broadcasts an <c>OrderCreated</c> event to all connected admin clients.
    /// Payload: { orderId, customerName, createdAt, itemCount }
    /// </summary>
    Task NotifyOrderCreatedAsync(
        Guid orderId,
        string customerName,
        DateTime createdAt,
        int itemCount,
        CancellationToken cancellationToken = default);
}
