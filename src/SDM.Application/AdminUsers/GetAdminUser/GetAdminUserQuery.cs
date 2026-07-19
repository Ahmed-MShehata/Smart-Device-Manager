using SDM.Application.Common.CQRS;

namespace SDM.Application.AdminUsers.GetAdminUser;

/// <summary>Query to retrieve a single admin user by ID.</summary>
public sealed class GetAdminUserQuery : IQuery<GetAdminUserResponse>
{
    public Guid Id { get; init; }
}
