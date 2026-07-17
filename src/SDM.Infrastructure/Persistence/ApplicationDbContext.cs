using Microsoft.EntityFrameworkCore;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence;

/// <summary>
/// Primary SQL Server database context for Smart Device Manager.
/// All entity configurations are applied from the Configurations folder via <c>ApplyConfigurationsFromAssembly</c>.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <inheritdoc/>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>Administrator accounts.</summary>
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    /// <summary>Hardware products available for customers to order.</summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>Customer purchase orders.</summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>Individual product lines within orders.</summary>
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    /// <summary>Software packages managed by admin for customer installation.</summary>
    public DbSet<SoftwarePackage> SoftwarePackages => Set<SoftwarePackage>();

    /// <summary>Required Windows runtime components.</summary>
    public DbSet<SystemComponent> SystemComponents => Set<SystemComponent>();

    /// <summary>Top-level diagnostic problem categories.</summary>
    public DbSet<DiagnosticCategory> DiagnosticCategories => Set<DiagnosticCategory>();

    /// <summary>MCQ questions within diagnostic categories.</summary>
    public DbSet<DiagnosticQuestion> DiagnosticQuestions => Set<DiagnosticQuestion>();

    /// <summary>Answer choices for diagnostic questions.</summary>
    public DbSet<DiagnosticChoice> DiagnosticChoices => Set<DiagnosticChoice>();

    /// <summary>Score-range rules mapping to diagnosis results.</summary>
    public DbSet<DiagnosticRule> DiagnosticRules => Set<DiagnosticRule>();

    /// <summary>System notifications for customers and admins.</summary>
    public DbSet<Notification> Notifications => Set<Notification>();

    /// <summary>Global key-value configuration settings.</summary>
    public DbSet<Setting> Settings => Set<Setting>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
