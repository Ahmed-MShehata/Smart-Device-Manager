using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.Orders.UpdateOrderStatus;

/// <summary>
/// Command that transitions an order to a new lifecycle status.
/// Rejects terminal orders (Delivered, Cancelled) at the handler level
/// before delegating to the domain's <c>UpdateStatus</c> method.
/// Handled by <see cref="UpdateOrderStatusHandler"/>.
/// Returns a non-generic <see cref="Common.Result"/>.
/// </summary>
public sealed class UpdateOrderStatusCommand : ICommand
{
    /// <summary>Gets the unique identifier of the order to update.</summary>
    public Guid OrderId { get; init; }

    /// <summary>Gets the target status to transition to.</summary>
    public OrderStatus NewStatus { get; init; }

    /// <summary>Gets optional admin notes to attach or update.</summary>
    public string? Notes { get; init; }
}
