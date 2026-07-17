using Microsoft.AspNetCore.Identity;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Services;

/// <summary>
/// Password hashing implementation backed by <see cref="PasswordHasher{TUser}"/> from ASP.NET Core Identity.
/// Uses PBKDF2 with a randomly generated salt by default — no custom algorithm required.
/// </summary>
internal sealed class PasswordHasherService : IPasswordHasher
{
    private readonly PasswordHasher<AdminUser> _hasher = new();

    /// <inheritdoc/>
    public string HashPassword(string password) =>
        _hasher.HashPassword(null!, password);

    /// <inheritdoc/>
    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
        return result is PasswordVerificationResult.Success
            or PasswordVerificationResult.SuccessRehashNeeded;
    }
}
