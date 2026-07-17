using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.Orders.GetOrders;

/// <summary>
/// Query that returns a paginated, filtered, and sorted list of orders.
/// Handled by <see cref="GetOrdersHandler"/>.
/// </summary>
public sealed class GetOrdersQuery : IQuery<PaginationResponse<GetOrdersResponse>>
{
    // ─── Pagination ───────────────────────────────────────────────────────────

    /// <summary>Gets the 1-based page number to retrieve. Default: 1.</summary>
    public int Page { get; init; } = 1;

    /// <summary>Gets the number of items per page. Range: 1–100. Default: 20.</summary>
    public int PageSize { get; init; } = 20;

    // ─── Search ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Gets an optional search term matched against customer name and phone number.
    /// Null or empty means no text filtering.
    /// </summary>
    public string? Search { get; init; }

    // ─── Filter ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Gets an optional status filter.
    /// Null means all statuses are included.
    /// </summary>
    public OrderStatus? Status { get; init; }

    // ─── Sort ─────────────────────────────────────────────────────────────────

    /// <summary>Gets the field to sort by. Default: <see cref="OrderSortBy.CreatedAt"/>.</summary>
    public OrderSortBy SortBy { get; init; } = OrderSortBy.CreatedAt;

    /// <summary>Gets the sort direction. Default: Descending (newest first).</summary>
    public bool Descending { get; init; } = true;
}
