using SDM.Domain.Common;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a global system configuration entry as a key-value pair.
/// Used for settings such as support phone, WhatsApp number, and company information.
/// </summary>
public class Setting : BaseEntity
{
    /// <summary>
    /// Gets the unique configuration key.
    /// Format: letters, digits, dots, and underscores only (e.g., "company.phone").
    /// </summary>
    public string Key { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the configuration value. Empty string means the setting is cleared.
    /// </summary>
    public string Value { get; private set; } = string.Empty;

    /// <summary>Gets an optional human-readable description of what this setting controls.</summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this setting is safe to expose via customer-facing API endpoints.
    /// False means admin-only.
    /// </summary>
    public bool IsPublic { get; private set; }

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected Setting() { }

    /// <summary>
    /// Creates a new <see cref="Setting"/>.
    /// </summary>
    /// <param name="key">Unique configuration key. Required. Format: alphanumeric, dots, underscores.</param>
    /// <param name="value">Configuration value. Empty string is allowed (clears the setting).</param>
    /// <param name="description">Optional description of what this setting controls.</param>
    /// <param name="isPublic">True if this setting can be exposed via customer-facing API. Defaults to false.</param>
    public Setting(string key, string value = "", string? description = null, bool isPublic = false)
    {
        Key = key;
        Value = value;
        Description = description;
        IsPublic = isPublic;
    }

    /// <summary>Updates the value of this setting.</summary>
    /// <param name="newValue">The new configuration value. Empty string clears the setting.</param>
    public void UpdateValue(string newValue) => Value = newValue;

    /// <summary>Updates the description and public visibility of this setting.</summary>
    public void UpdateMetadata(string? description, bool isPublic)
    {
        Description = description;
        IsPublic = isPublic;
    }
}
