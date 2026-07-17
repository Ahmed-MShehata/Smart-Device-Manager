using SDM.Domain.Entities;

namespace SDM.Application.Interfaces;

/// <summary>
/// Abstraction over JWT token generation.
/// The implementation uses <c>System.IdentityModel.Tokens.Jwt</c>.
/// This interface is responsible for token generation only.
/// Authentication logic remains in the Application command handler.
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Creates a signed JWT token for the specified admin user.
    /// </summary>
    /// <param name="adminUser">The authenticated admin user whose claims will be embedded in the token.</param>
    /// <returns>A signed JWT string ready to be returned to the client.</returns>
    string GenerateToken(AdminUser adminUser);
}
