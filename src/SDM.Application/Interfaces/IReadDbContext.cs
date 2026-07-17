using SDM.Domain.Entities;

namespace SDM.Application.Interfaces;

/// <summary>
/// Read-only view of the database context for use in query handlers.
/// Exposes entity sets as <see cref="IQueryable{T}"/> to support server-side
/// filtering, searching, sorting, and pagination via LINQ.
/// </summary>
/// <remarks>
/// <para>
/// This interface is the read side of the CQRS split:
/// <list type="bullet">
///   <item><description>
///     <b>Write side</b> — <see cref="IUnitOfWork"/> / <see cref="IRepository{T}"/>.
///     Used by command handlers. Supports Add, Update, Remove, SaveChanges.
///   </description></item>
///   <item><description>
///     <b>Read side</b> — <see cref="IReadDbContext"/>.
///     Used by query handlers. Read-only. No SaveChanges. No ChangeTracker.
///   </description></item>
/// </list>
/// </para>
/// <para>
/// The Infrastructure implementation (<c>ApplicationDbContext</c>) supplies
/// each set with <c>AsNoTracking()</c> applied, ensuring no entity is ever
/// change-tracked through this interface.
/// </para>
/// <para>
/// Query handlers must compose queries using standard LINQ extension methods
/// (<c>Where</c>, <c>OrderBy</c>, <c>Skip</c>, <c>Take</c>, <c>Select</c>).
/// Async materialisation (<c>CountAsync</c>, <c>ToListAsync</c>) is provided
/// by the EF Core extension methods available in the Infrastructure layer.
/// </para>
/// </remarks>
public interface IReadDbContext
{
    /// <summary>Queryable set of administrator accounts.</summary>
    IQueryable<AdminUser> AdminUsers { get; }

    /// <summary>Queryable set of hardware products.</summary>
    IQueryable<Product> Products { get; }

    /// <summary>Queryable set of customer purchase orders.</summary>
    IQueryable<Order> Orders { get; }

    /// <summary>Queryable set of individual order line items.</summary>
    IQueryable<OrderItem> OrderItems { get; }

    /// <summary>Queryable set of software packages.</summary>
    IQueryable<SoftwarePackage> SoftwarePackages { get; }

    /// <summary>Queryable set of required Windows system components.</summary>
    IQueryable<SystemComponent> SystemComponents { get; }

    /// <summary>Queryable set of diagnostic problem categories.</summary>
    IQueryable<DiagnosticCategory> DiagnosticCategories { get; }

    /// <summary>Queryable set of MCQ diagnostic questions.</summary>
    IQueryable<DiagnosticQuestion> DiagnosticQuestions { get; }

    /// <summary>Queryable set of answer choices for diagnostic questions.</summary>
    IQueryable<DiagnosticChoice> DiagnosticChoices { get; }

    /// <summary>Queryable set of score-range diagnostic rules.</summary>
    IQueryable<DiagnosticRule> DiagnosticRules { get; }

    /// <summary>Queryable set of system notifications.</summary>
    IQueryable<Notification> Notifications { get; }

    /// <summary>Queryable set of global key-value settings.</summary>
    IQueryable<Setting> Settings { get; }
}
