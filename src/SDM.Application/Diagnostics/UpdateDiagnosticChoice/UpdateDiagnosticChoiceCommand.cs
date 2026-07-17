using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.UpdateDiagnosticChoice;

/// <summary>
/// Command to update the text and score of a specific diagnostic choice.
/// </summary>
public sealed class UpdateDiagnosticChoiceCommand : ICommand
{
    /// <summary>Gets the choice identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the updated text for the choice.</summary>
    public string ChoiceText { get; init; } = string.Empty;

    /// <summary>Gets the updated score value.</summary>
    public int ScoreValue { get; init; }
}
