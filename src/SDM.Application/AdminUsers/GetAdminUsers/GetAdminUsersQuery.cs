using SDM.Application.Common;
using SDM.Application.Common.CQRS;

namespace SDM.Application.AdminUsers.GetAdminUsers;

/// <summary>Query to retrieve a paginated, searchable list of all admin users.</summary>
public sealed class GetAdminUsersQuery : IQuery<PaginationResponse<GetAdminUsersResponse>>
{
    /// <summary>Optional search term matched against username.</summary>
    public string? Search { get; init; }

    /// <summary>Page number (1-based). Default: 1.</summary>
    public int Page { get; init; } = 1;

    /// <summary>Items per page. Default: 20.</summary>
    public int PageSize { get; init; } = 20;
}
