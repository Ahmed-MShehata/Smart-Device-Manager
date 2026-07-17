using SDM.Domain.Enums;

namespace SDM.Application.AdminUsers.LoginAdminUser;

/// <summary>
/// Response payload returned after a successful authentication.
/// </summary>
public sealed class LoginAdminUserResponse
{
    /// <summary>Signed JWT string ready to be included in the Authorization header.</summary>
    public string Token { get; init; } = string.Empty;

    /// <summary>The authenticated user's ID.</summary>
    public Guid UserId { get; init; }

    /// <summary>The authenticated user's username.</summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>The assigned role.</summary>
    public AdminRole Role { get; init; }
}
