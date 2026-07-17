namespace SDM.Application.Diagnostics.GetDiagnosticCategories;

/// <summary>
/// Summary DTO for a single diagnostic category in the paginated list.
/// </summary>
public sealed class GetDiagnosticCategoriesResponse
{
    /// <summary>Unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Display name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Description of the problem area.</summary>
    public string? Description { get; init; }

    /// <summary>UI icon identifier.</summary>
    public string? IconName { get; init; }

    /// <summary>Whether the category is visible to customers.</summary>
    public bool IsActive { get; init; }

    /// <summary>Number of questions in this category.</summary>
    public int QuestionCount { get; init; }

    /// <summary>Number of scoring rules in this category.</summary>
    public int RuleCount { get; init; }
}
