using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.AdminUsers.GetAdminUsers;

/// <summary>Handles <see cref="GetAdminUsersQuery"/> with server-side pagination and search.</summary>
public sealed class GetAdminUsersHandler
    : IQueryHandler<GetAdminUsersQuery, PaginationResponse<GetAdminUsersResponse>>
{
    private readonly IReadDbContext _db;

    public GetAdminUsersHandler(IReadDbContext db) => _db = db;

    public async Task<Result<PaginationResponse<GetAdminUsersResponse>>> Handle(
        GetAdminUsersQuery query,
        CancellationToken cancellationToken)
    {
        var q = _db.AdminUsers.AsQueryable();

        // Search by username
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            q = q.Where(u => EF.Functions.Like(u.Username, $"%{query.Search}%"));
        }

        q = q.OrderBy(u => u.Username);

        var totalCount = await q.CountAsync(cancellationToken);

        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(u => new GetAdminUsersResponse
            {
                Id        = u.Id,
                Username  = u.Username,
                Role      = u.Role,
                IsActive  = u.IsActive,
                CreatedAt = u.CreatedAt,
                CreatedBy = u.CreatedBy,
                UpdatedAt = u.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        var pagination = PaginationRequest.Create(query.Page, query.PageSize);
        var response   = PaginationResponse<GetAdminUsersResponse>.Create(items, totalCount, pagination);

        return Result<PaginationResponse<GetAdminUsersResponse>>.Success(response);
    }
}
