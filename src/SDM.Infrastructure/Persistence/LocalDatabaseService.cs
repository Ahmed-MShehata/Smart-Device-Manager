using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SDM.Infrastructure.Persistence;

/// <summary>
/// Local SQLite database service for the Customer Application.
/// Provides offline-first data access. Tables will be added in later sprints.
/// </summary>
public class LocalDatabaseService
{
    private readonly string _connectionString;
    private readonly ILogger<LocalDatabaseService> _logger;

    public LocalDatabaseService(IConfiguration configuration, ILogger<LocalDatabaseService> logger)
    {
        _connectionString = configuration.GetConnectionString("LocalSQLite")
            ?? "Data Source=sdm_local.db";
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            _logger.LogInformation("Local SQLite database initialized successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize local SQLite database.");
            throw;
        }
    }
}
