using SDM.Application.Common.CQRS;

namespace SDM.Application.Settings.GetPublicSettings;

/// <summary>
/// Query to retrieve all settings where <c>IsPublic</c> is true.
/// Used by the customer application to download configuration data statically safely.
/// </summary>
public sealed class GetPublicSettingsQuery : IQuery<IReadOnlyList<GetPublicSettingsResponse>>
{
}
