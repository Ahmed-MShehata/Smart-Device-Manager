using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.SystemComponents.GetSystemComponents;

/// <summary>
/// Query to retrieve a paginated, filtered, and sorted list of system components.
/// </summary>
public sealed class GetSystemComponentsQuery : IQuery<PaginationResponse<GetSystemComponentsResponse>>
{
    // ─── Pagination ───────────────────────────────────────────────────────────

    /// <summary>Gets the 1-based page number to retrieve. Default: 1.</summary>
    public int Page { get; init; } = 1;

    /// <summary>Gets the number of items per page. Range: 1–100. Default: 20.</summary>
    public int PageSize { get; init; } = 20;

    // ─── Search & Filter ──────────────────────────────────────────────────────

    /// <summary>Optional search term (matches Name and Version).</summary>
    public string? Search { get; init; }

    /// <summary>Optional status filter.</summary>
    public ComponentStatus? Status { get; init; }

    // ─── Sort ─────────────────────────────────────────────────────────────────

    /// <summary>Gets the field to sort by. Default: <see cref="SystemComponentSortBy.Name"/>.</summary>
    public SystemComponentSortBy SortBy { get; init; } = SystemComponentSortBy.Name;

    /// <summary>Sort direction. False = Ascending, True = Descending. Default: false.</summary>
    public bool Descending { get; init; }
}
