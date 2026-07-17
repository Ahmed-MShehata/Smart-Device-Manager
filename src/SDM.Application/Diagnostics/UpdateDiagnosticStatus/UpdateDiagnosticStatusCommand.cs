using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.UpdateDiagnosticStatus;

/// <summary>
/// Command to toggle the active/inactive visibility of a diagnostic category.
/// </summary>
public sealed class UpdateDiagnosticStatusCommand : ICommand
{
    /// <summary>Gets the unique identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the desired active state. True = visible to customers.</summary>
    public bool IsActive { get; init; }
}
