using Microsoft.AspNetCore.SignalR;

namespace SDM.Infrastructure.Hubs;

/// <summary>
/// SignalR hub for real-time admin notifications.
///
/// Events published through this hub:
///
/// <list type="bullet">
///   <item>
///     <term>OrderCreated</term>
///     <description>
///       Fired after a customer successfully places an order.
///       Payload: { orderId, customerName, createdAt, itemCount }
///       Subscriber: Admin WPF shell — triggers a native Windows notification
///       and routes the admin to the Orders Management page on click.
///     </description>
///   </item>
/// </list>
///
/// No sign-in is required to connect from the Admin app — the WPF shell
/// attaches the admin JWT as a Bearer token in the SignalR connection query.
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
