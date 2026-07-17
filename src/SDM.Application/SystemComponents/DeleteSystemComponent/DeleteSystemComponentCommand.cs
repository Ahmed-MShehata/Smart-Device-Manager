using SDM.Application.Common.CQRS;

namespace SDM.Application.SystemComponents.DeleteSystemComponent;

/// <summary>
/// Command to soft-delete a system component.
/// This translates to setting its status to deprecated/inactive.
/// </summary>
public sealed class DeleteSystemComponentCommand : ICommand
{
    /// <summary>Gets the unique identifier of the component. Supplied by the route.</summary>
    public Guid Id { get; init; }
}
