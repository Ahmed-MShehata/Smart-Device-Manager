using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.AddDiagnosticChoice;

/// <summary>
/// Command to append a new answer choice to an existing diagnostic question.
/// </summary>
public sealed class AddDiagnosticChoiceCommand : ICommand<AddDiagnosticChoiceResponse>
{
    /// <summary>Gets the parent question identifier. Supplied by the route.</summary>
    public Guid QuestionId { get; init; }

    /// <summary>Gets the text for the choice.</summary>
    public string ChoiceText { get; init; } = string.Empty;

    /// <summary>Gets the score value applied if this choice is selected.</summary>
    public int ScoreValue { get; init; }
}
