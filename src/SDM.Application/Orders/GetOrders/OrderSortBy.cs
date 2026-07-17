namespace SDM.Application.Orders.GetOrders;

/// <summary>
/// Defines the fields by which the order list can be sorted.
/// Used in <see cref="GetOrdersQuery.SortBy"/>.
/// </summary>
public enum OrderSortBy
{
    /// <summary>Sort by creation date (default).</summary>
    CreatedAt = 0,

    /// <summary>Sort by customer name.</summary>
    CustomerName = 1,

    /// <summary>Sort by computed order total price.</summary>
    TotalPrice = 2
}
