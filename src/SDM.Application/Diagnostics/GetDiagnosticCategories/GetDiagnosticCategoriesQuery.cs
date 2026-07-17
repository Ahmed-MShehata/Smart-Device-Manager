using SDM.Application.Common;
using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.GetDiagnosticCategories;

/// <summary>
/// Query to retrieve a paginated list of all top-level diagnostic categories (summary view).
/// </summary>
public sealed class GetDiagnosticCategoriesQuery : IQuery<PaginationResponse<GetDiagnosticCategoriesResponse>>
{
    /// <summary>Gets the 1-based page number. Default: 1.</summary>
    public int Page { get; init; } = 1;

    /// <summary>Gets the page size. Range: 1–100. Default: 20.</summary>
    public int PageSize { get; init; } = 20;

    /// <summary>Optional search term matching category Name.</summary>
    public string? Search { get; init; }

    /// <summary>Optional filter by active/inactive state.</summary>
    public bool? IsActive { get; init; }
}
