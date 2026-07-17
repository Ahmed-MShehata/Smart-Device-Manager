namespace SDM.Domain.Enums;

/// <summary>Represents the lifecycle status of a customer order.</summary>
public enum OrderStatus
{
    /// <summary>Order has been submitted and is awaiting processing.</summary>
    Pending = 0,

    /// <summary>Order is being processed by admin.</summary>
    Processing = 1,

    /// <summary>Order has been shipped to the customer.</summary>
    Shipped = 2,

    /// <summary>Order has been delivered. Terminal state.</summary>
    Delivered = 3,

    /// <summary>Order has been cancelled. Terminal state.</summary>
    Cancelled = 4
}
