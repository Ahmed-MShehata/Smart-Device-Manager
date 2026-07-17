using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SDM.Application.Interfaces;
using SDM.Infrastructure.Services;

namespace SDM.Infrastructure.Persistence;

/// <summary>
/// Design-time factory for <see cref="ApplicationDbContext"/>.
/// Required by EF Core tooling (migrations) when the DbContext lives in a class library
/// separate from the startup project.
/// Uses a <see cref="DesignTimeCurrentUserService"/> stub since no HTTP context
/// exists at design time.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    /// <inheritdoc/>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=.;Database=SmartDeviceManagerDb;Trusted_Connection=True;TrustServerCertificate=True;",
            sql => sql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

        // Use design-time stubs — no HTTP context exists during migrations.
        return new ApplicationDbContext(
            optionsBuilder.Options,
            new DesignTimeCurrentUserService(),
            new DateTimeProvider());
    }

    // ─── Design-Time Stubs ────────────────────────────────────────────────────

    /// <summary>
    /// Null-object implementation of <see cref="ICurrentUserService"/> used exclusively
    /// by the design-time factory. Returns null for all identity properties so the
    /// DbContext falls back to the "system" username when stamping migrations seed data.
    /// </summary>
    private sealed class DesignTimeCurrentUserService : ICurrentUserService
    {
        /// <inheritdoc/>
        public string? Username => null;

        /// <inheritdoc/>
        public string? UserId => null;

        /// <inheritdoc/>
        public string? Role => null;

        /// <inheritdoc/>
        public bool IsAuthenticated => false;
    }
}
