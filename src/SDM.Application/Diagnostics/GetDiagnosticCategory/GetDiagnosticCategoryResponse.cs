namespace SDM.Application.Diagnostics.GetDiagnosticCategory;

/// <summary>
/// DTO for a single answer choice within a diagnostic question.
/// </summary>
public sealed class DiagnosticChoiceDto
{
    /// <summary>Unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Answer text.</summary>
    public string ChoiceText { get; init; } = string.Empty;

    /// <summary>Score contribution (can be negative).</summary>
    public int ScoreValue { get; init; }
}

/// <summary>
/// DTO for a single MCQ question within a diagnostic category.
/// </summary>
public sealed class DiagnosticQuestionDto
{
    /// <summary>Unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Question text.</summary>
    public string QuestionText { get; init; } = string.Empty;

    /// <summary>Display order index.</summary>
    public int OrderIndex { get; init; }

    /// <summary>All available answer choices.</summary>
    public IReadOnlyList<DiagnosticChoiceDto> Choices { get; init; } = [];
}

/// <summary>
/// DTO for a single scoring rule within a diagnostic category.
/// </summary>
public sealed class DiagnosticRuleDto
{
    /// <summary>Unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Lower bound score (inclusive).</summary>
    public int MinScore { get; init; }

    /// <summary>Upper bound score (inclusive).</summary>
    public int MaxScore { get; init; }

    /// <summary>Short diagnosis result label.</summary>
    public string Result { get; init; } = string.Empty;

    /// <summary>Detailed step-by-step solution.</summary>
    public string Solution { get; init; } = string.Empty;
}

/// <summary>
/// Full hierarchical diagnostic category response including nested questions, choices, and rules.
/// </summary>
public sealed class GetDiagnosticCategoryResponse
{
    /// <summary>Unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Category name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Description.</summary>
    public string? Description { get; init; }

    /// <summary>UI icon identifier.</summary>
    public string? IconName { get; init; }

    /// <summary>Visibility status.</summary>
    public bool IsActive { get; init; }

    /// <summary>Ordered list of MCQ questions.</summary>
    public IReadOnlyList<DiagnosticQuestionDto> Questions { get; init; } = [];

    /// <summary>Score range → result/solution rules.</summary>
    public IReadOnlyList<DiagnosticRuleDto> Rules { get; init; } = [];
}
