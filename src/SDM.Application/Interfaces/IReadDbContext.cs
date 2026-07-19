using SDM.Domain.Entities;

namespace SDM.Application.Interfaces;

/// <summary>
/// Read-only view of the database context for use in query handlers.
/// Exposes entity sets as <see cref="IQueryable{T}"/> to support server-side
/// filtering, searching, sorting, and pagination via LINQ.
/// Used exclusively by query handlers (read side of CQRS).
/// All sets are exposed with AsNoTracking() via the Infrastructure implementation.
/// </summary>
public interface IReadDbContext
{
    /// <summary>Queryable set of administrator accounts.</summary>
    IQueryable<AdminUser> AdminUsers { get; }

    /// <summary>Queryable set of hardware products available in the catalog.</summary>
    IQueryable<Product> Products { get; }

    /// <summary>Queryable set of customer purchase orders.</summary>
    IQueryable<Order> Orders { get; }

    /// <summary>Queryable set of individual order line items.</summary>
    IQueryable<OrderItem> OrderItems { get; }

    /// <summary>Queryable set of software packages (applications and drivers).</summary>
    IQueryable<SoftwarePackage> SoftwarePackages { get; }

    /// <summary>Queryable set of knowledge base articles.</summary>
    IQueryable<KnowledgeBaseArticle> KnowledgeBaseArticles { get; }

    /// <summary>Queryable set of company profiles.</summary>
    IQueryable<CompanyProfile> CompanyProfiles { get; }

    /// <summary>Queryable set of diagnostic problem categories.</summary>
    IQueryable<DiagnosticCategory> DiagnosticCategories { get; }

    /// <summary>Queryable set of MCQ diagnostic questions.</summary>
    IQueryable<DiagnosticQuestion> DiagnosticQuestions { get; }

    /// <summary>Queryable set of answer choices for diagnostic questions.</summary>
    IQueryable<DiagnosticChoice> DiagnosticChoices { get; }

    /// <summary>Queryable set of score-range diagnostic rules.</summary>
    IQueryable<DiagnosticRule> DiagnosticRules { get; }
}
