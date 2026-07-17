using SDM.Domain.Enums;

namespace SDM.Application.Orders.GetOrder;

/// <summary>
/// Full order detail DTO, including customer info, device reference, and all line items.
/// Returned by <see cref="GetOrderHandler"/>.
/// </summary>
public sealed class GetOrderResponse
{
    /// <summary>Gets the unique identifier of the order.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the full name of the customer.</summary>
    public string CustomerName { get; init; } = string.Empty;

    /// <summary>Gets the customer's phone number.</summary>
    public string PhoneNumber { get; init; } = string.Empty;

    /// <summary>Gets the delivery address.</summary>
    public string Address { get; init; } = string.Empty;

    /// <summary>Gets the customer's device identifier.</summary>
    public string DeviceId { get; init; } = string.Empty;

    /// <summary>Gets the current lifecycle status of the order.</summary>
    public OrderStatus Status { get; init; }

    /// <summary>Gets optional admin notes. Null if not set.</summary>
    public string? Notes { get; init; }

    /// <summary>Gets the total price of the order (sum of all line totals).</summary>
    public decimal TotalPrice { get; init; }

    /// <summary>Gets the UTC timestamp when the order was created.</summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>Gets the UTC timestamp when the order was last updated. Null if never updated.</summary>
    public DateTime? UpdatedAt { get; init; }

    /// <summary>Gets the administrator who created the record. Empty for customer-submitted orders.</summary>
    public string CreatedBy { get; init; } = string.Empty;

    /// <summary>Gets the line items belonging to this order.</summary>
    public IReadOnlyList<OrderItemResponse> Items { get; init; } = [];
}

/// <summary>
/// DTO representing a single product line within an order detail response.
/// Prices reflect the snapshot captured at order creation time.
/// </summary>
public sealed class OrderItemResponse
{
    /// <summary>Gets the unique identifier of the order item.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the unique identifier of the referenced product.</summary>
    public Guid ProductId { get; init; }

    /// <summary>Gets the display name of the product at the time of the query.</summary>
    public string ProductName { get; init; } = string.Empty;

    /// <summary>Gets the number of units ordered.</summary>
    public int Quantity { get; init; }

    /// <summary>Gets the unit price snapshot at the time the order was created.</summary>
    public decimal Price { get; init; }

    /// <summary>Gets the total price for this line (Price × Quantity).</summary>
    public decimal LineTotal { get; init; }
}
