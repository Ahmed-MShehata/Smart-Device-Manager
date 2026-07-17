namespace SDM.Application.Diagnostics.AddDiagnosticQuestion;

/// <summary>
/// Confirmation response after adding a question.
/// </summary>
public sealed class AddDiagnosticQuestionResponse
{
    /// <summary>Newly assigned question ID.</summary>
    public Guid QuestionId { get; init; }

    /// <summary>Count of choices persisted.</summary>
    public int ChoiceCount { get; init; }
}
