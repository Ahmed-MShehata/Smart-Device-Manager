namespace SDM.Application.Settings.GetSettings;

/// <summary>
/// Complete overview DTO for retrieving settings in an admin view.
/// </summary>
public sealed class GetSettingsResponse
{
    /// <summary>Unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Setting key.</summary>
    public string Key { get; init; } = string.Empty;

    /// <summary>Setting value.</summary>
    public string Value { get; init; } = string.Empty;

    /// <summary>Description.</summary>
    public string? Description { get; init; }

    /// <summary>Whether this is exposed to public API.</summary>
    public bool IsPublic { get; init; }
}
