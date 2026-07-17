using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDM.Infrastructure.Persistence;

namespace SDM.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // SQL Server Database
        services.AddDatabase(configuration);

        // SignalR (registered at API level via builder.Services.AddSignalR())
        // JWT (registered at API level via builder.Services.AddJwtAuthentication())

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        });

        return services;
    }
}
