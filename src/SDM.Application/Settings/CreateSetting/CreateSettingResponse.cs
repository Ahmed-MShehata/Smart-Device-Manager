namespace SDM.Application.Settings.CreateSetting;

/// <summary>
/// Confirmation response returning the newly created setting info.
/// </summary>
public sealed class CreateSettingResponse
{
    /// <summary>Gets the unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the configuration key.</summary>
    public string Key { get; init; } = string.Empty;
}
