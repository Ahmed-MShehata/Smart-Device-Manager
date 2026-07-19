using SDM.Application.Common;
using SDM.Application.Common.CQRS;

namespace SDM.Application.SoftwarePackages.GetSoftwarePackages;

/// <summary>Query to retrieve a paginated, filtered, and sorted list of software packages.</summary>
public sealed class GetSoftwarePackagesQuery : IQuery<PaginationResponse<GetSoftwarePackagesResponse>>
{
    /// <summary>Gets the 1-based page number. Default: 1.</summary>
    public int Page { get; init; } = 1;

    /// <summary>Gets the items per page (1–100). Default: 20.</summary>
    public int PageSize { get; init; } = 20;

    /// <summary>Optional search term (matches Name and Category).</summary>
    public string? Search { get; init; }

    /// <summary>Optional category filter: "Application" or "Driver".</summary>
    public string? Category { get; init; }

    /// <summary>Gets the field to sort by. Default: Name.</summary>
    public SoftwarePackageSortBy SortBy { get; init; } = SoftwarePackageSortBy.Name;

    /// <summary>Sort direction. False = Ascending, True = Descending.</summary>
    public bool Descending { get; init; }
}
