namespace SDM.Application.Interfaces;

/// <summary>
/// Abstraction over password hashing.
/// The implementation uses <c>Microsoft.AspNetCore.Identity.PasswordHasher&lt;T&gt;</c>.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>Hashes a plain-text password and returns the resulting hash string.</summary>
    /// <param name="password">The plain-text password to hash. Must not be null or empty.</param>
    /// <returns>An opaque hash string suitable for storage.</returns>
    string HashPassword(string password);

    /// <summary>Verifies a plain-text password against a previously hashed value.</summary>
    /// <param name="hashedPassword">The stored hash to compare against.</param>
    /// <param name="providedPassword">The plain-text password supplied by the user.</param>
    /// <returns><see langword="true"/> if the password matches the hash; otherwise <see langword="false"/>.</returns>
    bool VerifyPassword(string hashedPassword, string providedPassword);
}
