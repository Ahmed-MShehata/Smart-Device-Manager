using SDM.Domain.Enums;

namespace SDM.Application.SystemComponents.GetSystemComponents;

/// <summary>
/// Summary DTO for a single system component in a paginated list.
/// </summary>
public sealed class GetSystemComponentsResponse
{
    /// <summary>Unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Display name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Version string.</summary>
    public string Version { get; init; } = string.Empty;

    /// <summary>Size in bytes.</summary>
    public long Size { get; init; }

    /// <summary>Availability status.</summary>
    public ComponentStatus Status { get; init; }

    /// <summary>UTC creation time.</summary>
    public DateTime CreatedAt { get; init; }
}
