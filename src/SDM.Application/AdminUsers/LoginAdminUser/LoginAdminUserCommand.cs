using SDM.Application.Common.CQRS;

namespace SDM.Application.AdminUsers.LoginAdminUser;

/// <summary>
/// Command to authenticate an administrator and produce a signed JWT token.
/// Authentication is treated as a command because it mutates implicit state
/// (session context, audit log) and is not idempotent.
/// </summary>
public sealed class LoginAdminUserCommand : ICommand<LoginAdminUserResponse>
{
    /// <summary>Gets the login username.</summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>Gets the plain-text password to verify.</summary>
    public string Password { get; init; } = string.Empty;
}
