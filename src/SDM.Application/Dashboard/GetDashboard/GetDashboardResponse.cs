using SDM.Domain.Enums;

namespace SDM.Application.Dashboard.GetDashboard;

/// <summary>Dashboard summary DTO returned to the Admin Application.</summary>
public sealed class GetDashboardResponse
{
    public int TotalProducts { get; init; }
    public int TotalSoftwarePackages { get; init; }
    public int TotalOrders { get; init; }
    public int PendingOrders { get; init; }
    public int CompletedOrders { get; init; }
    public int KnowledgeBaseCount { get; init; }
    public int DiagnosticCategoriesCount { get; init; }
    public IReadOnlyList<RecentOrderDto> RecentOrders { get; init; } = [];
}

/// <summary>Lightweight order summary for the dashboard recent orders list.</summary>
public sealed class RecentOrderDto
{
    public Guid Id { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string CustomerPhone { get; init; } = string.Empty;
    public OrderStatus Status { get; init; }
    public decimal TotalPrice { get; init; }
    public DateTime CreatedAt { get; init; }
}
