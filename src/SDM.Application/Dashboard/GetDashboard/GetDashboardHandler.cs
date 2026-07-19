using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Enums;

namespace SDM.Application.Dashboard.GetDashboard;

/// <summary>
/// Handles <see cref="GetDashboardQuery"/>.
/// Executes multiple lightweight COUNT queries and a single capped recent-orders projection.
/// All queries run via <see cref="IReadDbContext"/> (AsNoTracking, read-only).
/// </summary>
public sealed class GetDashboardHandler : IQueryHandler<GetDashboardQuery, GetDashboardResponse>
{
    private readonly IReadDbContext _db;

    public GetDashboardHandler(IReadDbContext db) => _db = db;

    public async Task<Result<GetDashboardResponse>> Handle(
        GetDashboardQuery query,
        CancellationToken cancellationToken)
    {
        var recentCount = Math.Clamp(query.RecentOrdersCount, 1, 50);

        var totalProducts             = await _db.Products.CountAsync(cancellationToken);
        var totalSoftwarePackages     = await _db.SoftwarePackages.CountAsync(cancellationToken);
        var totalOrders               = await _db.Orders.CountAsync(cancellationToken);
        var pendingOrders             = await _db.Orders.CountAsync(o => o.Status == OrderStatus.Pending, cancellationToken);
        var completedOrders           = await _db.Orders.CountAsync(o => o.Status == OrderStatus.Delivered, cancellationToken);
        var kbCount                   = await _db.KnowledgeBaseArticles.CountAsync(cancellationToken);
        var diagnosticsCategoriesCount= await _db.DiagnosticCategories.CountAsync(cancellationToken);

        var recentOrders = await _db.Orders
            .OrderByDescending(o => o.CreatedAt)
            .Take(recentCount)
            .Select(o => new RecentOrderDto
            {
                Id           = o.Id,
                CustomerName = o.CustomerName,
                CustomerPhone = o.CustomerPhone,
                Status       = o.Status,
                TotalPrice   = o.Items.Sum(i => i.Price * i.Quantity),
                CreatedAt    = o.CreatedAt
            })
            .ToListAsync(cancellationToken);

        var response = new GetDashboardResponse
        {
            TotalProducts             = totalProducts,
            TotalSoftwarePackages     = totalSoftwarePackages,
            TotalOrders               = totalOrders,
            PendingOrders             = pendingOrders,
            CompletedOrders           = completedOrders,
            KnowledgeBaseCount        = kbCount,
            DiagnosticCategoriesCount = diagnosticsCategoriesCount,
            RecentOrders              = recentOrders
        };

        return Result<GetDashboardResponse>.Success(response);
    }
}
