using SDM.Application.Common.CQRS;

namespace SDM.Application.Orders.CancelOrder;

/// <summary>
/// Command that cancels an existing order by transitioning it to
/// <see cref="Domain.Enums.OrderStatus.Cancelled"/>.
/// This is a soft delete — order data is fully preserved.
/// Handled by <see cref="CancelOrderHandler"/>.
/// Returns a non-generic <see cref="Common.Result"/>.
/// </summary>
public sealed class CancelOrderCommand : ICommand
{
    /// <summary>Gets the unique identifier of the order to cancel.</summary>
    public Guid OrderId { get; init; }
}
