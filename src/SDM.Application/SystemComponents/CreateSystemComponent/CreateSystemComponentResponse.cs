using SDM.Domain.Enums;

namespace SDM.Application.SystemComponents.CreateSystemComponent;

/// <summary>
/// Confirmation response returning the newly created component info.
/// </summary>
public sealed class CreateSystemComponentResponse
{
    /// <summary>Gets the unique identifier of the new component.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the component name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the component version.</summary>
    public string Version { get; init; } = string.Empty;

    /// <summary>Gets the current status.</summary>
    public ComponentStatus Status { get; init; }

    /// <summary>Gets the UTC creation timestamp.</summary>
    public DateTime CreatedAt { get; init; }
}
