using SDM.Domain.Common;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a single product line within an <see cref="Order"/>.
/// Stores a price snapshot at the time of order creation — independent of future product price changes.
/// Inherits <see cref="BaseEntity"/> (no audit trail needed — items are append-only).
/// </summary>
public class OrderItem : BaseEntity
{
    /// <summary>Gets the identifier of the parent order.</summary>
    public Guid OrderId { get; private set; }

    /// <summary>Gets the identifier of the referenced product.</summary>
    public Guid ProductId { get; private set; }

    /// <summary>Gets the number of units ordered. Must be at least 1.</summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets the unit price at the time the order was created.
    /// This is a snapshot and does not change if the product price changes later.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Gets the total price for this line.
    /// Computed: Price × Quantity. Not persisted.
    /// </summary>
    public decimal LineTotal => Price * Quantity;

    // Navigation properties
    /// <summary>Navigation property to the parent order. Populated by EF Core.</summary>
    public Order? Order { get; private set; }

    /// <summary>Navigation property to the referenced product. Populated by EF Core.</summary>
    public Product? Product { get; private set; }

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected OrderItem() { }

    /// <summary>
    /// Creates a new <see cref="OrderItem"/>.
    /// </summary>
    /// <param name="orderId">The Guid of the parent order.</param>
    /// <param name="productId">The Guid of the product being ordered.</param>
    /// <param name="quantity">Number of units. Must be at least 1.</param>
    /// <param name="price">Unit price at order creation. Must be greater than zero.</param>
    public OrderItem(Guid orderId, Guid productId, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}
