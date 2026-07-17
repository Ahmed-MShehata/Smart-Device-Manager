namespace SDM.Application.Diagnostics.AddDiagnosticChoice;

/// <summary>
/// Confirmation response after adding a generic choice.
/// </summary>
public sealed class AddDiagnosticChoiceResponse
{
    /// <summary>Newly assigned choice identifier.</summary>
    public Guid ChoiceId { get; init; }
}
