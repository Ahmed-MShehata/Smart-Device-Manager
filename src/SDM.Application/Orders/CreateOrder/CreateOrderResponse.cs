using SDM.Domain.Enums;

namespace SDM.Application.Orders.CreateOrder;

/// <summary>
/// DTO returned after an order is successfully created.
/// Provides the caller with order confirmation details.
/// </summary>
public sealed class CreateOrderResponse
{
    /// <summary>Gets the unique identifier of the newly created order.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the full name of the customer who placed the order.</summary>
    public string CustomerName { get; init; } = string.Empty;

    /// <summary>Gets the current order status (always <see cref="OrderStatus.Pending"/> on creation).</summary>
    public OrderStatus Status { get; init; }

    /// <summary>Gets the calculated total price of the order at creation time.</summary>
    public decimal TotalPrice { get; init; }

    /// <summary>Gets the number of distinct product lines in the order.</summary>
    public int ItemCount { get; init; }

    /// <summary>Gets the UTC timestamp when the order was created.</summary>
    public DateTime CreatedAt { get; init; }
}
