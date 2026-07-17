using SDM.Domain.Common;
using SDM.Domain.Enums;
using SDM.Domain.ValueObjects;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a customer hardware purchase transaction.
/// Contains customer contact details, a device reference, and one or more order items.
/// Audit fields (<c>CreatedBy</c>, <c>UpdatedBy</c>, <c>UpdatedAt</c>) are stamped
/// automatically by Infrastructure — never by this entity.
/// </summary>
public class Order : AuditableEntity
{
    private readonly HashSet<OrderItem> _items = [];

    /// <summary>Gets the full name of the customer who placed this order.</summary>
    public string CustomerName { get; private set; } = string.Empty;

    /// <summary>Gets the phone number of the customer.</summary>
    public string PhoneNumber { get; private set; } = string.Empty;

    /// <summary>Gets the delivery address of the customer.</summary>
    public string Address { get; private set; } = string.Empty;

    /// <summary>Gets the reference to the customer's device that initiated this order.</summary>
    public DeviceReference Device { get; private set; } = null!;

    /// <summary>Gets the current lifecycle status of this order.</summary>
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    /// <summary>Gets optional notes added by admin. Null if not set.</summary>
    public string? Notes { get; private set; }

    /// <summary>
    /// Gets the read-only collection of items in this order.
    /// An order must have at least one item.
    /// </summary>
    public IReadOnlyCollection<OrderItem> Items => _items;

    /// <summary>
    /// Gets the total price of this order.
    /// Computed: sum of (OrderItem.Price × OrderItem.Quantity). Not persisted.
    /// </summary>
    public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity);

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected Order() { }

    /// <summary>
    /// Creates a new <see cref="Order"/>.
    /// Status defaults to <see cref="OrderStatus.Pending"/>.
    /// Audit fields are stamped by Infrastructure on save.
    /// </summary>
    /// <param name="customerName">Full name of the customer. Required.</param>
    /// <param name="phoneNumber">Customer phone number. Required.</param>
    /// <param name="address">Delivery address. Required.</param>
    /// <param name="device">Reference to the customer's device. Required.</param>
    public Order(
        string customerName,
        string phoneNumber,
        string address,
        DeviceReference device)
    {
        CustomerName = customerName;
        PhoneNumber = phoneNumber;
        Address = address;
        Device = device;
        Status = OrderStatus.Pending;
    }

    /// <summary>
    /// Adds an item to this order.
    /// </summary>
    /// <param name="item">The <see cref="OrderItem"/> to add. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown when item is null.</exception>
    public void AddItem(OrderItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        _items.Add(item);
    }

    /// <summary>
    /// Advances the order to a new status.
    /// Terminal statuses (Delivered, Cancelled) cannot be changed.
    /// </summary>
    /// <param name="newStatus">The target <see cref="OrderStatus"/>.</param>
    /// <exception cref="InvalidOperationException">Thrown when the order is in a terminal state.</exception>
    public void UpdateStatus(OrderStatus newStatus)
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException(
                $"Cannot change status of an order that is already {Status}.");

        Status = newStatus;
    }

    /// <summary>Sets or updates the admin notes on this order.</summary>
    /// <param name="notes">The note text. Null clears the notes.</param>
    public void SetNotes(string? notes)
    {
        Notes = notes;
    }
}
