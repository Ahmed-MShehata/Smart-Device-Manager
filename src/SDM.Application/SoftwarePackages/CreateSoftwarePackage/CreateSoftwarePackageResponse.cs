namespace SDM.Application.SoftwarePackages.CreateSoftwarePackage;

/// <summary>Response returned after successfully uploading a new software package.</summary>
public sealed class CreateSoftwarePackageResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
