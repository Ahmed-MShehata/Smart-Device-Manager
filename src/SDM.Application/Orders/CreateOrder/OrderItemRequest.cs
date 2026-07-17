namespace SDM.Application.Orders.CreateOrder;

/// <summary>
/// Input DTO representing a single product line in a <see cref="CreateOrderCommand"/>.
/// Each entry maps to one <c>OrderItem</c> domain object.
/// </summary>
public sealed class OrderItemRequest
{
    /// <summary>Gets the unique identifier of the product being ordered.</summary>
    public Guid ProductId { get; init; }

    /// <summary>Gets the number of units to order. Must be at least 1.</summary>
    public int Quantity { get; init; }
}
