using Microsoft.EntityFrameworkCore;
using SDM.Application.Interfaces;
using SDM.Domain.Common;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence;

/// <summary>
/// Primary SQL Server database context for Smart Device Manager.
/// Implements two interfaces:
///   - Write side: used by UnitOfWork / Repository&lt;T&gt; for command operations.
///   - Read side: implements <see cref="IReadDbContext"/> for query handlers.
///     All sets exposed via the read interface use AsNoTracking().
/// Automatically stamps audit fields on every <see cref="AuditableEntity"/> during
/// <see cref="SaveChangesAsync"/> via <see cref="ICurrentUserService"/> and
/// <see cref="IDateTimeProvider"/>.
/// Entity configurations are applied from the Configurations folder via
/// ApplyConfigurationsFromAssembly.
/// </summary>
public class ApplicationDbContext : DbContext, IReadDbContext
{
    private readonly ICurrentUserService _currentUser;
    private readonly IDateTimeProvider _dateTime;

    /// <inheritdoc/>
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUser,
        IDateTimeProvider dateTime)
        : base(options)
    {
        _currentUser = currentUser;
        _dateTime = dateTime;
    }

    // ─── Write-Side DbSets (used by UnitOfWork / Repository<T>) ──────────────

    /// <summary>Administrator accounts.</summary>
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    /// <summary>Hardware products available for customers to order.</summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>Customer purchase orders.</summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>Individual product lines within orders.</summary>
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    /// <summary>Software packages and drivers managed by admin.</summary>
    public DbSet<SoftwarePackage> SoftwarePackages => Set<SoftwarePackage>();

    /// <summary>Knowledge base troubleshooting articles.</summary>
    public DbSet<KnowledgeBaseArticle> KnowledgeBaseArticles => Set<KnowledgeBaseArticle>();

    /// <summary>Company profile displayed to customers.</summary>
    public DbSet<CompanyProfile> CompanyProfiles => Set<CompanyProfile>();

    /// <summary>Top-level diagnostic problem categories.</summary>
    public DbSet<DiagnosticCategory> DiagnosticCategories => Set<DiagnosticCategory>();

    /// <summary>MCQ questions within diagnostic categories.</summary>
    public DbSet<DiagnosticQuestion> DiagnosticQuestions => Set<DiagnosticQuestion>();

    /// <summary>Answer choices for diagnostic questions.</summary>
    public DbSet<DiagnosticChoice> DiagnosticChoices => Set<DiagnosticChoice>();

    /// <summary>Score-range rules mapping to diagnosis results.</summary>
    public DbSet<DiagnosticRule> DiagnosticRules => Set<DiagnosticRule>();

    // ─── Read-Side (IReadDbContext — AsNoTracking enforced) ───────────────────

    IQueryable<AdminUser>            IReadDbContext.AdminUsers             => AdminUsers.AsNoTracking();
    IQueryable<Product>              IReadDbContext.Products               => Products.AsNoTracking();
    IQueryable<Order>                IReadDbContext.Orders                 => Orders.AsNoTracking();
    IQueryable<OrderItem>            IReadDbContext.OrderItems             => OrderItems.AsNoTracking();
    IQueryable<SoftwarePackage>      IReadDbContext.SoftwarePackages       => SoftwarePackages.AsNoTracking();
    IQueryable<KnowledgeBaseArticle> IReadDbContext.KnowledgeBaseArticles  => KnowledgeBaseArticles.AsNoTracking();
    IQueryable<CompanyProfile>       IReadDbContext.CompanyProfiles         => CompanyProfiles.AsNoTracking();
    IQueryable<DiagnosticCategory>   IReadDbContext.DiagnosticCategories    => DiagnosticCategories.AsNoTracking();
    IQueryable<DiagnosticQuestion>   IReadDbContext.DiagnosticQuestions     => DiagnosticQuestions.AsNoTracking();
    IQueryable<DiagnosticChoice>     IReadDbContext.DiagnosticChoices       => DiagnosticChoices.AsNoTracking();
    IQueryable<DiagnosticRule>       IReadDbContext.DiagnosticRules         => DiagnosticRules.AsNoTracking();

    // ─── Audit Stamping ───────────────────────────────────────────────────────

    /// <summary>
    /// Persists all pending changes to the database.
    /// Automatically stamps <see cref="AuditableEntity"/> audit fields before saving.
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        StampAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    // ─── Private ──────────────────────────────────────────────────────────────

    private void StampAuditFields()
    {
        var username = _currentUser.Username ?? "system";
        var now = _dateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(AuditableEntity.CreatedBy)).CurrentValue = username;
                entry.Property(nameof(BaseEntity.CreatedAt)).CurrentValue = now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(AuditableEntity.UpdatedBy)).CurrentValue = username;
                entry.Property(nameof(AuditableEntity.UpdatedAt)).CurrentValue = now;
            }
        }
    }
}
