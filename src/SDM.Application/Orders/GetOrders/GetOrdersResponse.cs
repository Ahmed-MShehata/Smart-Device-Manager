using SDM.Domain.Enums;

namespace SDM.Application.Orders.GetOrders;

/// <summary>
/// Summary DTO for a single order in the paginated order list.
/// Does not include item details — use <c>GetOrderQuery</c> for full details.
/// </summary>
public sealed class GetOrdersResponse
{
    /// <summary>Gets the unique identifier of the order.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the full name of the customer.</summary>
    public string CustomerName { get; init; } = string.Empty;

    /// <summary>Gets the customer's phone number.</summary>
    public string PhoneNumber { get; init; } = string.Empty;

    /// <summary>Gets the current lifecycle status of the order.</summary>
    public OrderStatus Status { get; init; }

    /// <summary>Gets the calculated total price (sum of all line totals).</summary>
    public decimal TotalPrice { get; init; }

    /// <summary>Gets the number of distinct product lines in the order.</summary>
    public int ItemCount { get; init; }

    /// <summary>Gets the UTC timestamp when the order was created.</summary>
    public DateTime CreatedAt { get; init; }
}
