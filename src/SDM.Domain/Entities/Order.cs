using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a customer purchase order.
/// Customer contact information (name, phone, WhatsApp, governorate, address)
/// is collected from the locally stored onboarding profile and submitted
/// with every order. No authentication or account is involved.
/// Audit fields are stamped automatically by Infrastructure on save.
/// </summary>
public class Order : AuditableEntity
{
    private readonly HashSet<OrderItem> _items = [];

    /// <summary>Gets the full name of the customer who placed this order.</summary>
    public string CustomerName { get; private set; } = string.Empty;

    /// <summary>Gets the phone number of the customer.</summary>
    public string CustomerPhone { get; private set; } = string.Empty;

    /// <summary>Gets the WhatsApp number of the customer. Optional.</summary>
    public string? CustomerWhatsApp { get; private set; }

    /// <summary>Gets the governorate (region) of the customer. Required.</summary>
    public string CustomerGovernorate { get; private set; } = string.Empty;

    /// <summary>Gets the delivery address of the customer.</summary>
    public string CustomerAddress { get; private set; } = string.Empty;

    /// <summary>Gets the current lifecycle status of this order.</summary>
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

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
    /// <param name="customerName">Full name. Required.</param>
    /// <param name="customerPhone">Phone number. Required.</param>
    /// <param name="customerWhatsApp">WhatsApp number. Optional.</param>
    /// <param name="customerGovernorate">Governorate / region. Required.</param>
    /// <param name="customerAddress">Delivery address. Required.</param>
    public Order(
        string customerName,
        string customerPhone,
        string? customerWhatsApp,
        string customerGovernorate,
        string customerAddress)
    {
        CustomerName = customerName;
        CustomerPhone = customerPhone;
        CustomerWhatsApp = customerWhatsApp;
        CustomerGovernorate = customerGovernorate;
        CustomerAddress = customerAddress;
        Status = OrderStatus.Pending;
    }

    /// <summary>Adds an item to this order.</summary>
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
    /// <exception cref="InvalidOperationException">Thrown when order is in terminal state.</exception>
    public void UpdateStatus(OrderStatus newStatus)
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException(
                $"Cannot change status of an order that is already {Status}.");

        Status = newStatus;
    }
}
