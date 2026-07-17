using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.Notifications.GetNotifications;

/// <summary>
/// Retrieves paginated notifications filtered securely for the requesting user/device.
/// </summary>
public sealed class GetNotificationsQuery : IQuery<PaginationResponse<GetNotificationsResponse>>
{
    // ─── Pagination ───────────────────────────────────────────────────────────

    /// <summary>Gets the 1-based page number to retrieve. Default: 1.</summary>
    public int Page { get; init; } = 1;

    /// <summary>Gets the number of items per page. Range: 1–100. Default: 20.</summary>
    public int PageSize { get; init; } = 20;

    // ─── Filters ──────────────────────────────────────────────────────────────

    /// <summary>Target audience to retrieve. E.g., 'Customer' or 'Admin'.</summary>
    public NotificationTarget Target { get; init; } = NotificationTarget.Admin;

    /// <summary>Optional. If set, restricts results strictly to exact matches + broadcasts for this device.</summary>
    public string? DeviceId { get; init; }

    /// <summary>Optional. Filters purely by unread state.</summary>
    public bool? UnreadOnly { get; init; }
}
