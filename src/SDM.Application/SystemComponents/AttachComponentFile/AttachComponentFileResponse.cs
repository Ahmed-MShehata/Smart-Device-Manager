namespace SDM.Application.SystemComponents.AttachComponentFile;

/// <summary>Confirmation response after attaching a file to a system component.</summary>
public sealed class AttachComponentFileResponse
{
    /// <summary>Newly assigned file record identifier.</summary>
    public Guid FileId { get; init; }
}
