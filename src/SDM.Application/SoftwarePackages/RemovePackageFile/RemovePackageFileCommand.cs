using SDM.Application.Common.CQRS;

namespace SDM.Application.SoftwarePackages.RemovePackageFile;

/// <summary>
/// Command to remove a file reference from a software package.
/// </summary>
public sealed class RemovePackageFileCommand : ICommand
{
    /// <summary>Gets the file record identifier.</summary>
    public Guid Id { get; init; }
}
