using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.AdminUsers.CreateAdminUser;

/// <summary>
/// Command to provision a new administrative account.
/// Restricted to SuperAdmins only.
/// </summary>
public sealed class CreateAdminUserCommand : ICommand<CreateAdminUserResponse>
{
    /// <summary>Gets the unique login username. Required. Length: 3–50.</summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>Gets the plain-text initial password. Will be hashed before storage.</summary>
    public string Password { get; init; } = string.Empty;

    /// <summary>Gets the access role to assign. Default: Admin.</summary>
    public AdminRole Role { get; init; } = AdminRole.Admin;
}
