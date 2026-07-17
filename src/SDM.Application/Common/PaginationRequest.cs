namespace SDM.Application.Common;

/// <summary>
/// Represents a pagination request from the client.
/// Used as a base input for all paginated queries.
/// </summary>
public sealed class PaginationRequest
{
    /// <summary>Gets the 1-based page number to retrieve. Minimum: 1.</summary>
    public int Page { get; init; } = 1;

    /// <summary>Gets the number of items per page. Default: 20. Maximum: 100.</summary>
    public int PageSize { get; init; } = 20;

    /// <summary>Gets the zero-based offset for database skip operations.</summary>
    public int Skip => (Page - 1) * PageSize;

    /// <summary>
    /// Creates a <see cref="PaginationRequest"/> with the given page and page size,
    /// clamping values to valid ranges.
    /// </summary>
    /// <param name="page">The page number. Clamped to minimum 1.</param>
    /// <param name="pageSize">The page size. Clamped to range 1–100.</param>
    public static PaginationRequest Create(int page, int pageSize) => new()
    {
        Page = Math.Max(1, page),
        PageSize = Math.Clamp(pageSize, 1, 100)
    };
}
