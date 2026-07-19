using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDM.Application.Interfaces;

namespace SDM.API.Controllers;

/// <summary>
/// Minimal health check endpoint.
/// No authentication required — used by infrastructure monitors and the desktop app startup checks.
/// </summary>
[ApiController]
[Route("api/health")]
[AllowAnonymous]
public sealed class HealthController : ControllerBase
{
    private readonly IReadDbContext _db;

    public HealthController(IReadDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Returns the current server status, application version, database connectivity, and UTC time.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(HealthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HealthResponse), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<HealthResponse>> GetHealth(CancellationToken cancellationToken)
    {
        var version = typeof(HealthController).Assembly.GetName().Version?.ToString() ?? "1.0.0";

        bool dbHealthy;
        string dbStatus;

        try
        {
            // A lightweight connectivity check — does not load any entity data
            dbHealthy = await _db.Products.AnyAsync(cancellationToken) || true;
            dbStatus  = "Healthy";
        }
        catch (Exception ex)
        {
            dbHealthy = false;
            dbStatus  = $"Unhealthy: {ex.Message}";
        }

        var response = new HealthResponse
        {
            Status          = dbHealthy ? "Healthy" : "Degraded",
            ApplicationVersion = version,
            DatabaseStatus  = dbStatus,
            UtcTime         = DateTime.UtcNow
        };

        return dbHealthy
            ? Ok(response)
            : StatusCode(StatusCodes.Status503ServiceUnavailable, response);
    }
}

/// <summary>Health check response payload.</summary>
public sealed class HealthResponse
{
    public string Status { get; init; } = string.Empty;
    public string ApplicationVersion { get; init; } = string.Empty;
    public string DatabaseStatus { get; init; } = string.Empty;
    public DateTime UtcTime { get; init; }
}
