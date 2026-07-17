using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.UpdateDiagnosticQuestion;

/// <summary>
/// Command to update the text and ordering of an existing MCQ question.
/// Note: Answer choices are structurally replaced via a separate or atomic flow if needed.
/// This command focuses strictly on the question metadata.
/// </summary>
public sealed class UpdateDiagnosticQuestionCommand : ICommand
{
    /// <summary>Gets the question identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the updated question text.</summary>
    public string QuestionText { get; init; } = string.Empty;

    /// <summary>Gets the updated order index within its category.</summary>
    public int OrderIndex { get; init; }
}
