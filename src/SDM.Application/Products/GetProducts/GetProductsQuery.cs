using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.Products.GetProducts;

/// <summary>
/// Query that returns a paginated, filtered, and sorted list of products.
/// Handled by <see cref="GetProductsHandler"/>.
/// Returns <see cref="Result{T}"/> wrapping a <see cref="PaginationResponse{T}"/>
/// of <see cref="GetProductsResponse"/> items.
/// </summary>
public sealed class GetProductsQuery : IQuery<PaginationResponse<GetProductsResponse>>
{
    // ─── Pagination ───────────────────────────────────────────────────────────

    /// <summary>Gets the 1-based page number to retrieve. Default: 1.</summary>
    public int Page { get; init; } = 1;

    /// <summary>Gets the number of items per page. Range: 1–100. Default: 10.</summary>
    public int PageSize { get; init; } = 10;

    // ─── Search ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Gets an optional search term matched against product name and brand.
    /// Null or empty means no filtering.
    /// </summary>
    public string? Search { get; init; }

    // ─── Filter ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Gets an optional status filter.
    /// Null means all statuses are included.
    /// </summary>
    public ProductStatus? Status { get; init; }

    // ─── Sort ─────────────────────────────────────────────────────────────────

    /// <summary>Gets the field to sort by. Default: <see cref="ProductSortBy.Name"/>.</summary>
    public ProductSortBy SortBy { get; init; } = ProductSortBy.Name;

    /// <summary>Gets the sort direction. Default: <see cref="SortDirection.Ascending"/>.</summary>
    public SortDirection SortDirection { get; init; } = SortDirection.Ascending;
}
