using SDM.Domain.Common;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a top-level grouping for the offline MCQ diagnostic problem tree
/// (e.g., Overheating, Network Issues, Performance, Windows Problems).
/// Contains <see cref="DiagnosticQuestion"/>s and <see cref="DiagnosticRule"/>s.
/// </summary>
public class DiagnosticCategory : BaseEntity
{
    private readonly HashSet<DiagnosticQuestion> _questions = [];
    private readonly HashSet<DiagnosticRule> _rules = [];

    /// <summary>Gets the unique display name of this category.</summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>Gets an optional description of the problem area. Null if not set.</summary>
    public string? Description { get; private set; }

    /// <summary>Gets an optional UI icon identifier for this category. Null if not set.</summary>
    public string? IconName { get; private set; }

    /// <summary>Gets a value indicating whether this category is visible to customers.</summary>
    public bool IsActive { get; private set; } = true;

    /// <summary>Gets the read-only collection of questions in this category.</summary>
    public IReadOnlyCollection<DiagnosticQuestion> Questions => _questions;

    /// <summary>Gets the read-only collection of scoring rules in this category.</summary>
    public IReadOnlyCollection<DiagnosticRule> Rules => _rules;

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected DiagnosticCategory() { }

    /// <summary>
    /// Creates a new <see cref="DiagnosticCategory"/>.
    /// </summary>
    /// <param name="name">Unique category name. Required.</param>
    /// <param name="description">Optional description of the problem area.</param>
    /// <param name="iconName">Optional UI icon identifier.</param>
    public DiagnosticCategory(string name, string? description = null, string? iconName = null)
    {
        Name = name;
        Description = description;
        IconName = iconName;
        IsActive = true;
    }

    /// <summary>Adds a diagnostic question to this category.</summary>
    /// <param name="question">The <see cref="DiagnosticQuestion"/> to add.</param>
    public void AddQuestion(DiagnosticQuestion question)
    {
        ArgumentNullException.ThrowIfNull(question);
        _questions.Add(question);
    }

    /// <summary>Adds a diagnostic scoring rule to this category.</summary>
    /// <param name="rule">The <see cref="DiagnosticRule"/> to add.</param>
    public void AddRule(DiagnosticRule rule)
    {
        ArgumentNullException.ThrowIfNull(rule);
        _rules.Add(rule);
    }

    /// <summary>Makes this category visible to customers.</summary>
    public void Activate() => IsActive = true;

    /// <summary>Hides this category from customers without deleting it.</summary>
    public void Deactivate() => IsActive = false;

    /// <summary>Updates the editable metadata of this category.</summary>
    public void Update(string name, string? description, string? iconName)
    {
        Name = name;
        Description = description;
        IconName = iconName;
    }
}
