using SDM.Application.Common;
using SDM.Application.Common.CQRS;

namespace SDM.Application.Settings.GetSettings;

/// <summary>
/// Query to retrieve a paginated view of all global settings.
/// </summary>
public sealed class GetSettingsQuery : IQuery<PaginationResponse<GetSettingsResponse>>
{
    // ─── Pagination ───────────────────────────────────────────────────────────

    /// <summary>Gets the 1-based page number to retrieve. Default: 1.</summary>
    public int Page { get; init; } = 1;

    /// <summary>Gets the number of items per page. Range: 1–100. Default: 20.</summary>
    public int PageSize { get; init; } = 20;

    // ─── Search & Filter ──────────────────────────────────────────────────────

    /// <summary>Optional search term targeting Key or Description.</summary>
    public string? Search { get; init; }

    /// <summary>Optional filter by public availability.</summary>
    public bool? IsPublic { get; init; }
}
