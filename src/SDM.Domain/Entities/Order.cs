using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

public class Order : BaseEntity
{
    public string CustomerName { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string DeviceId { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public List<OrderItem> Items { get; private set; } = [];

    protected Order() { }

    public Order(string customerName, string phoneNumber, string address, string deviceId)
    {
        CustomerName = customerName;
        PhoneNumber = phoneNumber;
        Address = address;
        DeviceId = deviceId;
        Status = OrderStatus.Pending;
    }

    public void AddItem(OrderItem item) => Items.Add(item);
    public void UpdateStatus(OrderStatus status) => Status = status;
    public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
}

public class OrderItem : BaseEntity
{
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public Order? Order { get; private set; }
    public Product? Product { get; private set; }

    protected OrderItem() { }

    public OrderItem(int orderId, int productId, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}
