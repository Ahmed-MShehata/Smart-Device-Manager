using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Enums;

namespace SDM.Application.Notifications.GetNotifications;

/// <summary>
/// Queries <see cref="IReadDbContext"/>, isolating notifications firmly for their requested roles or explicit <c>DeviceId</c>.
/// Automatically bubbles pinned messages ahead of newly generated messages.
/// </summary>
public sealed class GetNotificationsHandler : IQueryHandler<GetNotificationsQuery, PaginationResponse<GetNotificationsResponse>>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetNotificationsHandler"/>.</summary>
    public GetNotificationsHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<PaginationResponse<GetNotificationsResponse>>> Handle(
        GetNotificationsQuery query,
        CancellationToken cancellationToken)
    {
        var q = _db.Notifications.AsQueryable();

        // 1. Audience Enformcement
        if (query.DeviceId != null)
        {
            // If device ID is provided, fetch messages specifically for this device, or broadcasts to its target group (e.g. all Customers).
            q = q.Where(n => n.Target == query.Target && (n.DeviceId == query.DeviceId || n.DeviceId == null));
        }
        else
        {
            // Match pure target Enum
            q = q.Where(n => n.Target == query.Target && n.DeviceId == null);
        }

        // 2. Unread filtering
        if (query.UnreadOnly.HasValue && query.UnreadOnly.Value)
        {
            q = q.Where(n => !n.IsRead);
        }

        // 3. Priority + Time Sorting: Pinned first, then newest first
        q = q.OrderByDescending(n => n.IsPinned).ThenByDescending(n => n.CreatedAt);

        // Capture total
        var totalCount = await q.CountAsync(cancellationToken);

        // Fetch
        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(n => new GetNotificationsResponse
            {
                Id        = n.Id,
                Title     = n.Title,
                Message   = n.Message,
                IsRead    = n.IsRead,
                IsPinned  = n.IsPinned,
                DeviceId  = n.DeviceId,
                Target    = n.Target,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync(cancellationToken);

        var pagination = PaginationRequest.Create(query.Page, query.PageSize);
        var response = PaginationResponse<GetNotificationsResponse>.Create(items, totalCount, pagination);

        return Result<PaginationResponse<GetNotificationsResponse>>.Success(response);
    }
}
