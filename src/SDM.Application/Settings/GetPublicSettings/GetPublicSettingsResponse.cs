namespace SDM.Application.Settings.GetPublicSettings;

/// <summary>
/// Projection DTO for public settings containing strictly the Key and Value.
/// </summary>
public sealed class GetPublicSettingsResponse
{
    /// <summary>Gets the configuration key.</summary>
    public string Key { get; init; } = string.Empty;

    /// <summary>Gets the configuration value.</summary>
    public string Value { get; init; } = string.Empty;
}
