using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.AdminUsers.GetAdminUser;

/// <summary>Handles <see cref="GetAdminUserQuery"/>.</summary>
public sealed class GetAdminUserHandler : IQueryHandler<GetAdminUserQuery, GetAdminUserResponse>
{
    private readonly IReadDbContext _db;

    public GetAdminUserHandler(IReadDbContext db) => _db = db;

    public async Task<Result<GetAdminUserResponse>> Handle(
        GetAdminUserQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _db.AdminUsers
            .Where(u => u.Id == query.Id)
            .Select(u => new GetAdminUserResponse
            {
                Id        = u.Id,
                Username  = u.Username,
                Role      = u.Role,
                IsActive  = u.IsActive,
                CreatedAt = u.CreatedAt,
                CreatedBy = u.CreatedBy,
                UpdatedAt = u.UpdatedAt,
                UpdatedBy = u.UpdatedBy
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result<GetAdminUserResponse>.Failure(Error.NotFound("AdminUser"));

        return Result<GetAdminUserResponse>.Success(response);
    }
}
