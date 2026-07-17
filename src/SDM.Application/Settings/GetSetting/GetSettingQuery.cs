using SDM.Application.Common.CQRS;

namespace SDM.Application.Settings.GetSetting;

/// <summary>
/// Query to retrieve exactly one setting by its GUID ID.
/// </summary>
public sealed class GetSettingQuery : IQuery<GetSettingResponse>
{
    /// <summary>Gets the unique identifier of the setting.</summary>
    public Guid Id { get; init; }
}
