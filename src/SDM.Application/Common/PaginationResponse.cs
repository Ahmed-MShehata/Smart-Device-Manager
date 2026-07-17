namespace SDM.Application.Common;

/// <summary>
/// Represents a paginated response wrapping a data collection with metadata.
/// Returned by all paginated queries.
/// </summary>
/// <typeparam name="T">The type of items in the paginated result.</typeparam>
public sealed class PaginationResponse<T>
{
    /// <summary>Gets the items for the current page.</summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>Gets the total number of items across all pages.</summary>
    public int TotalCount { get; }

    /// <summary>Gets the current page number (1-based).</summary>
    public int Page { get; }

    /// <summary>Gets the number of items per page.</summary>
    public int PageSize { get; }

    /// <summary>Gets the total number of pages.</summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>Gets a value indicating whether there is a previous page.</summary>
    public bool HasPreviousPage => Page > 1;

    /// <summary>Gets a value indicating whether there is a next page.</summary>
    public bool HasNextPage => Page < TotalPages;

    /// <summary>
    /// Initializes a new <see cref="PaginationResponse{T}"/>.
    /// </summary>
    /// <param name="items">The items on the current page.</param>
    /// <param name="totalCount">The total number of items across all pages.</param>
    /// <param name="page">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    public PaginationResponse(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }

    /// <summary>
    /// Creates a <see cref="PaginationResponse{T}"/> from a list and a <see cref="PaginationRequest"/>.
    /// </summary>
    public static PaginationResponse<T> Create(
        IReadOnlyList<T> items,
        int totalCount,
        PaginationRequest request)
        => new(items, totalCount, request.Page, request.PageSize);
}
