using SDM.Domain.Enums;

namespace SDM.Application.AdminUsers.GetAdminUser;

/// <summary>Full detail DTO for a single admin user.</summary>
public sealed class GetAdminUserResponse
{
    public Guid Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public AdminRole Role { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public string CreatedBy { get; init; } = string.Empty;
    public DateTime? UpdatedAt { get; init; }
    public string? UpdatedBy { get; init; }
}
