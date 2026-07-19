using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.AdminUsers.UpdateAdminUser;

/// <summary>
/// Command to update the username, role, and password of an existing admin account.
/// Restricted to SuperAdmins only.
/// </summary>
public sealed class UpdateAdminUserCommand : ICommand
{
    /// <summary>Gets the admin user identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the new username. Must be unique.</summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>Gets the new role to assign.</summary>
    public AdminRole Role { get; init; }

    /// <summary>Gets the new plain-text password. Leave empty to keep existing password.</summary>
    public string? NewPassword { get; init; }
}
