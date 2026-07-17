using SDM.Application.Common.CQRS;

namespace SDM.Application.Settings.DeleteSetting;

/// <summary>
/// Command to hard-delete a setting.
/// </summary>
public sealed class DeleteSettingCommand : ICommand
{
    /// <summary>Gets the unique identifier of the setting. Supplied by the route.</summary>
    public Guid Id { get; init; }
}
