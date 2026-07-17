using SDM.Domain.Enums;

namespace SDM.Application.AdminUsers.CreateAdminUser;

/// <summary>
/// Confirmation response after provisioning a new admin account.
/// </summary>
public sealed class CreateAdminUserResponse
{
    /// <summary>Newly assigned unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>The username registered.</summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>The role assigned.</summary>
    public AdminRole Role { get; init; }
}
