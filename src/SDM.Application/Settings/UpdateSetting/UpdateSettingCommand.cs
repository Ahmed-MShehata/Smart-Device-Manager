using SDM.Application.Common.CQRS;

namespace SDM.Application.Settings.UpdateSetting;

/// <summary>
/// Command to update an existing configuration setting.
/// </summary>
public sealed class UpdateSettingCommand : ICommand
{
    /// <summary>Gets the unique identifier of the setting. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the configuration value. Can be empty.</summary>
    public string Value { get; init; } = string.Empty;

    /// <summary>Gets an optional description of what this setting controls.</summary>
    public string? Description { get; init; }

    /// <summary>Gets a value indicating whether this setting is safe to expose via public API.</summary>
    public bool IsPublic { get; init; }
}
