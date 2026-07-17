using Microsoft.AspNetCore.Http;
using SDM.Application.Interfaces;
using System.Security.Claims;

namespace SDM.Infrastructure.Services;

/// <summary>
/// ASP.NET Core implementation of <see cref="ICurrentUserService"/>.
/// Reads the authenticated admin's identity from <see cref="IHttpContextAccessor"/>.
/// </summary>
internal sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>Initializes a new instance of <see cref="CurrentUserService"/>.</summary>
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    /// <inheritdoc/>
    public string? Username =>
        User?.FindFirstValue(ClaimTypes.Name);

    /// <inheritdoc/>
    public string? UserId =>
        User?.FindFirstValue(ClaimTypes.NameIdentifier);

    /// <inheritdoc/>
    public string? Role =>
        User?.FindFirstValue(ClaimTypes.Role);

    /// <inheritdoc/>
    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated ?? false;
}
