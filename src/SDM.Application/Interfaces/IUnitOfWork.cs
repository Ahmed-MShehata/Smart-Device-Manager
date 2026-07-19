using SDM.Domain.Entities;

namespace SDM.Application.Interfaces;

/// <summary>
/// Unit of Work interface — aggregates repositories and coordinates a single database transaction.
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

    /// <summary>Repository for <see cref="KnowledgeBaseArticle"/> entities.</summary>
    IRepository<KnowledgeBaseArticle> KnowledgeBaseArticles { get; }

    /// <summary>Repository for <see cref="CompanyProfile"/> entities.</summary>
    IRepository<CompanyProfile> CompanyProfiles { get; }

    /// <summary>Repository for <see cref="DiagnosticCategory"/> entities.</summary>
    IRepository<DiagnosticCategory> DiagnosticCategories { get; }

    /// <summary>Repository for <see cref="DiagnosticQuestion"/> entities.</summary>
    IRepository<DiagnosticQuestion> DiagnosticQuestions { get; }

    /// <summary>Repository for <see cref="DiagnosticChoice"/> entities.</summary>
    IRepository<DiagnosticChoice> DiagnosticChoices { get; }

    /// <summary>Repository for <see cref="DiagnosticRule"/> entities.</summary>
    IRepository<DiagnosticRule> DiagnosticRules { get; }

    /// <summary>Persists all pending changes to the database.</summary>
    /// <returns>The number of state entries written.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
