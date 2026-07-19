using SDM.Domain.Enums;

namespace SDM.Application.AdminUsers.GetAdminUsers;

/// <summary>DTO returned in the paginated list of admin users.</summary>
public sealed class GetAdminUsersResponse
{
    public Guid Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public AdminRole Role { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public string CreatedBy { get; init; } = string.Empty;
    public DateTime? UpdatedAt { get; init; }
}
