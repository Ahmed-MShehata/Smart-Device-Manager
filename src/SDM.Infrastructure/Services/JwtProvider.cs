using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SDM.Infrastructure.Services;

/// <summary>
/// JWT token generation implementation.
/// Reads issuer, audience, secret key, and expiration from configuration.
/// Authentication logic is NOT performed here — only signing and serialisation.
/// </summary>
internal sealed class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;

    /// <summary>Initializes a new instance of <see cref="JwtProvider"/>.</summary>
    public JwtProvider(IConfiguration configuration) => _configuration = configuration;

    /// <inheritdoc/>
    public string GenerateToken(AdminUser adminUser)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var secretKey  = jwtSection["SecretKey"]  ?? throw new InvalidOperationException("JWT SecretKey is not configured.");
        var issuer     = jwtSection["Issuer"]     ?? "SDM.API";
        var audience   = jwtSection["Audience"]   ?? "SDM.Client";
        var expiryMins = int.TryParse(jwtSection["ExpiryMinutes"], out var mins) ? mins : 480;

        var signingKey    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCreds  = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,  adminUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
            new Claim("sdm_userid",                 adminUser.Id.ToString()),
            new Claim("sdm_username",               adminUser.Username),
            new Claim(ClaimTypes.Role,              adminUser.Role.ToString()),
            new Claim("sdm_role",                   adminUser.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer:             issuer,
            audience:           audience,
            claims:             claims,
            notBefore:          DateTime.UtcNow,
            expires:            DateTime.UtcNow.AddMinutes(expiryMins),
            signingCredentials: signingCreds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
