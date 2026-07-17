using SDM.Application.Common.CQRS;

namespace SDM.Application.SystemComponents.RemoveComponentFile;

/// <summary>Command to remove a file reference from a system component.</summary>
public sealed class RemoveComponentFileCommand : ICommand
{
    /// <summary>Gets the file record identifier.</summary>
    public Guid Id { get; init; }
}
