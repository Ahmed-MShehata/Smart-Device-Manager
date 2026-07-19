using SDM.Application.Common.CQRS;

namespace SDM.Application.Dashboard.GetDashboard;

/// <summary>Query to retrieve the admin dashboard summary.</summary>
public sealed class GetDashboardQuery : IQuery<GetDashboardResponse>
{
    /// <summary>Number of recent orders to include. Default: 10.</summary>
    public int RecentOrdersCount { get; init; } = 10;
}
