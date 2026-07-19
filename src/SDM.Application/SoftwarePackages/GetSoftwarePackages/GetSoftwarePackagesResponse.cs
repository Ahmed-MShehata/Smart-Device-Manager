namespace SDM.Application.SoftwarePackages.GetSoftwarePackages;

/// <summary>Summary DTO for a single software package in the paginated list.</summary>
public sealed class GetSoftwarePackagesResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
    public string? IconUrl { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
