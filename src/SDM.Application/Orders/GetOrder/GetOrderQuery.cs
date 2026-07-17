using SDM.Application.Common.CQRS;

namespace SDM.Application.Orders.GetOrder;

/// <summary>
/// Query that retrieves full details for a single order, including all line items.
/// Handled by <see cref="GetOrderHandler"/>.
/// </summary>
public sealed class GetOrderQuery : IQuery<GetOrderResponse>
{
    /// <summary>Gets the unique identifier of the order to retrieve.</summary>
    public Guid Id { get; init; }
}
