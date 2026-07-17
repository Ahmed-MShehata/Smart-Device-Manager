using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.AdminUsers.LoginAdminUser;

/// <summary>
/// Handles <see cref="LoginAdminUserCommand"/>.
/// Validates credentials, enforces active account requirement, and delegates token generation
/// to <see cref="IJwtProvider"/>. Authentication logic lives entirely in this handler.
/// </summary>
public sealed class LoginAdminUserHandler : ICommandHandler<LoginAdminUserCommand, LoginAdminUserResponse>
{
    private readonly IReadDbContext _db;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    /// <summary>Initializes a new instance of <see cref="LoginAdminUserHandler"/>.</summary>
    public LoginAdminUserHandler(IReadDbContext db, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _db = db;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    /// <inheritdoc/>
    public async Task<Result<LoginAdminUserResponse>> Handle(LoginAdminUserCommand command, CancellationToken cancellationToken)
    {
        // 1. Locate account by username (case-sensitive match)
        var adminUser = await _db.AdminUsers
            .Where(u => u.Username == command.Username)
            .FirstOrDefaultAsync(cancellationToken);

        // Deliberately vague error to prevent username enumeration
        if (adminUser is null)
            return Result<LoginAdminUserResponse>.Failure(
                Error.Unauthorized("Invalid username or password."));

        // 2. Reject suspended accounts
        if (!adminUser.IsActive)
            return Result<LoginAdminUserResponse>.Failure(
                Error.Unauthorized("This account has been deactivated. Contact a SuperAdmin."));

        // 3. Verify password
        var passwordValid = _passwordHasher.VerifyPassword(adminUser.PasswordHash, command.Password);
        if (!passwordValid)
            return Result<LoginAdminUserResponse>.Failure(
                Error.Unauthorized("Invalid username or password."));

        // 4. Delegate token generation to IJwtProvider
        var token = _jwtProvider.GenerateToken(adminUser);

        return Result<LoginAdminUserResponse>.Success(
            new LoginAdminUserResponse
            {
                Token    = token,
                UserId   = adminUser.Id,
                Username = adminUser.Username,
                Role     = adminUser.Role
            },
            "Login successful.");
    }
}
