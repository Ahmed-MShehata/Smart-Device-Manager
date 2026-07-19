using SDM.Application.Common.CQRS;

namespace SDM.Application.Orders.CreateOrder;

/// <summary>
/// Command that creates a new customer order.
/// Customer-facing — used by the anonymous order submission endpoint.
/// All contact fields are sourced from the locally stored customer onboarding profile.
/// Handled by <see cref="CreateOrderHandler"/>.
/// Returns a <see cref="CreateOrderResponse"/> wrapped in <see cref="Common.Result{T}"/>.
/// </summary>
public sealed class CreateOrderCommand : ICommand<CreateOrderResponse>
{
    /// <summary>Gets the full name of the customer. Required.</summary>
    public string CustomerName { get; init; } = string.Empty;

    /// <summary>Gets the customer's phone number. Required.</summary>
    public string CustomerPhone { get; init; } = string.Empty;

    /// <summary>Gets the customer's WhatsApp number. Optional.</summary>
    public string? CustomerWhatsApp { get; init; }

    /// <summary>Gets the customer's governorate / region. Required.</summary>
    public string CustomerGovernorate { get; init; } = string.Empty;

    /// <summary>Gets the customer's delivery address. Required.</summary>
    public string CustomerAddress { get; init; } = string.Empty;

    /// <summary>
    /// Gets the list of products and quantities being ordered.
    /// Must contain at least one item.
    /// </summary>
    public IReadOnlyList<OrderItemRequest> Items { get; init; } = [];
}
