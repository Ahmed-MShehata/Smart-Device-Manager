using SDM.Application.Common.CQRS;

namespace SDM.Application.Settings.CreateSetting;

/// <summary>
/// Command to create a new global configuration setting.
/// </summary>
public sealed class CreateSettingCommand : ICommand<CreateSettingResponse>
{
    /// <summary>Gets the unique configuration key. Required.</summary>
    public string Key { get; init; } = string.Empty;

    /// <summary>Gets the configuration value. Can be empty.</summary>
    public string Value { get; init; } = string.Empty;

    /// <summary>Gets an optional description of what this setting controls.</summary>
    public string? Description { get; init; }

    /// <summary>Gets a value indicating whether this setting is safe to expose via public API.</summary>
    public bool IsPublic { get; init; }
}
