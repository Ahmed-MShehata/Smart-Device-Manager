using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.DeleteDiagnosticChoice;

/// <summary>
/// Command to remove an existing diagnostic choice completely.
/// </summary>
public sealed class DeleteDiagnosticChoiceCommand : ICommand
{
    /// <summary>Gets the choice identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }
}
