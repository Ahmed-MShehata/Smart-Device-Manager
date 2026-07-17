using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.SystemComponents.UpdateComponentStatus;

/// <summary>
/// Command to change the availability status of a system component.
/// </summary>
public sealed class UpdateComponentStatusCommand : ICommand
{
    /// <summary>Gets the unique identifier of the component. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the new status to apply.</summary>
    public ComponentStatus Status { get; init; }
}
