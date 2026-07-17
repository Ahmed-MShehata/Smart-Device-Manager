using SDM.Domain.Entities;

namespace SDM.Application.Interfaces;

/// <summary>
/// Unit of Work interface — aggregates repositories and coordinates a single database transaction.
/// Ensures all changes within one logical operation are committed or rolled back together.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>Repository for <see cref="AdminUser"/> entities.</summary>
    IRepository<AdminUser> AdminUsers { get; }

    /// <summary>Repository for <see cref="Product"/> entities.</summary>
    IRepository<Product> Products { get; }

    /// <summary>Repository for <see cref="Order"/> entities.</summary>
    IRepository<Order> Orders { get; }

    /// <summary>Repository for <see cref="OrderItem"/> entities.</summary>
    IRepository<OrderItem> OrderItems { get; }

    /// <summary>Repository for <see cref="SoftwarePackage"/> entities.</summary>
    IRepository<SoftwarePackage> SoftwarePackages { get; }

    /// <summary>Repository for <see cref="SystemComponent"/> entities.</summary>
    IRepository<SystemComponent> SystemComponents { get; }

    /// <summary>Repository for <see cref="PackageFile"/> entities.</summary>
    IRepository<PackageFile> PackageFiles { get; }

    /// <summary>Repository for <see cref="ComponentFile"/> entities.</summary>
    IRepository<ComponentFile> ComponentFiles { get; }

    /// <summary>Repository for <see cref="DiagnosticCategory"/> entities.</summary>
    IRepository<DiagnosticCategory> DiagnosticCategories { get; }

    /// <summary>Repository for <see cref="DiagnosticQuestion"/> entities.</summary>
    IRepository<DiagnosticQuestion> DiagnosticQuestions { get; }

    /// <summary>Repository for <see cref="DiagnosticChoice"/> entities.</summary>
    IRepository<DiagnosticChoice> DiagnosticChoices { get; }

    /// <summary>Repository for <see cref="DiagnosticRule"/> entities.</summary>
    IRepository<DiagnosticRule> DiagnosticRules { get; }

    /// <summary>Repository for <see cref="Notification"/> entities.</summary>
    IRepository<Notification> Notifications { get; }

    /// <summary>Repository for <see cref="Setting"/> entities.</summary>
    IRepository<Setting> Settings { get; }

    /// <summary>
    /// Persists all pending changes to the database.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
