using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.AddDiagnosticQuestion;

/// <summary>
/// Represents a single choice input within a question creation request.
/// </summary>
public sealed class DiagnosticChoiceRequest
{
    /// <summary>Gets the answer text. Required.</summary>
    public string ChoiceText { get; init; } = string.Empty;

    /// <summary>Gets the score contribution. Can be negative.</summary>
    public int ScoreValue { get; init; }
}

/// <summary>
/// Command to add a new MCQ question (with its choices) to an existing diagnostic category.
/// </summary>
public sealed class AddDiagnosticQuestionCommand : ICommand<AddDiagnosticQuestionResponse>
{
    /// <summary>Gets the parent category identifier. Supplied by the route.</summary>
    public Guid CategoryId { get; init; }

    /// <summary>Gets the question text displayed to the customer.</summary>
    public string QuestionText { get; init; } = string.Empty;

    /// <summary>Gets the display order index (zero-based) within the category.</summary>
    public int OrderIndex { get; init; }

    /// <summary>Gets the answer choices. Must contain at least 2 choices.</summary>
    public IReadOnlyList<DiagnosticChoiceRequest> Choices { get; init; } = [];
}
