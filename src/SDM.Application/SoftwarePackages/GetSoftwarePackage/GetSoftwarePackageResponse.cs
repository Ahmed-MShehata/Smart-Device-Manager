namespace SDM.Application.SoftwarePackages.GetSoftwarePackage;

/// <summary>Full detail DTO for a single software package.</summary>
public sealed class GetSoftwarePackageResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string? IconUrl { get; init; }
    public string SetupFileUrl { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public string CreatedBy { get; init; } = string.Empty;
    public DateTime? UpdatedAt { get; init; }
    public string? UpdatedBy { get; init; }
}
