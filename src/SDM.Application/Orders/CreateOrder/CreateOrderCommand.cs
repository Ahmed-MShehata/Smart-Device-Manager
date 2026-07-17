using SDM.Application.Common.CQRS;

namespace SDM.Application.Orders.CreateOrder;

/// <summary>
/// Command that creates a new customer order.
/// Customer-facing — used by the anonymous order submission endpoint.
/// Handled by <see cref="CreateOrderHandler"/>.
/// Returns a <see cref="CreateOrderResponse"/> wrapped in <see cref="Common.Result{T}"/>.
/// </summary>
public sealed class CreateOrderCommand : ICommand<CreateOrderResponse>
{
    /// <summary>Gets the full name of the customer placing the order. Required.</summary>
    public string CustomerName { get; init; } = string.Empty;

    /// <summary>Gets the customer's contact phone number. Required.</summary>
    public string PhoneNumber { get; init; } = string.Empty;

    /// <summary>Gets the delivery address. Required.</summary>
    public string Address { get; init; } = string.Empty;

    /// <summary>Gets the unique identifier of the customer's device. Required.</summary>
    public string DeviceId { get; init; } = string.Empty;

    /// <summary>Gets optional notes to attach to the order.</summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets the list of products and quantities being ordered.
    /// Must contain at least one item.
    /// </summary>
    public IReadOnlyList<OrderItemRequest> Items { get; init; } = [];
}
