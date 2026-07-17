namespace SDM.Application.Settings.GetSetting;

/// <summary>
/// Detail DTO for a specific setting.
/// </summary>
public sealed class GetSettingResponse
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

    /// <summary>UTC creation time.</summary>
    public DateTime CreatedAt { get; init; }
}
