using SDM.Application.Common.CQRS;

namespace SDM.Application.SystemComponents.GetComponentFiles;

/// <summary>Query to retrieve all file metadata records for a specific system component.</summary>
public sealed class GetComponentFilesQuery : IQuery<List<ComponentFileRecord>>
{
    /// <summary>Gets the parent component identifier.</summary>
    public Guid ComponentId { get; init; }
}
