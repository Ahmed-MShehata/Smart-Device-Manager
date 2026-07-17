using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDM.Application.Interfaces;
using SDM.Infrastructure.Persistence;
using SDM.Infrastructure.Repositories;
using SDM.Infrastructure.Services;

namespace SDM.Infrastructure.Extensions;

/// <summary>
/// Extension methods to register all Infrastructure layer services
/// into the ASP.NET Core dependency injection container.
/// </summary>
public static class InfrastructureServiceExtensions
{
    /// <summary>
    /// Registers the database context, repositories, unit of work, and shared services.
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddSharedServices();

        // SignalR — registered at API level via builder.Services.AddSignalR()
        // JWT     — registered at API level via builder.Services.AddAuthentication()

        return services;
    }

    // ─── Private Helpers ──────────────────────────────────────────────────────

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found in configuration.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);

                sqlOptions.MigrationsAssembly(
                    typeof(ApplicationDbContext).Assembly.FullName);
            });
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Generic repository — open generic registration
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Unit of Work — scoped per HTTP request
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        // Required for CurrentUserService to access HttpContext
        services.AddHttpContextAccessor();

        // Abstraction over system clock — injectable and testable
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        // Provides authenticated admin identity to Application layer handlers
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
