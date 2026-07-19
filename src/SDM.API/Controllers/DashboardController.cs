using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Dashboard.GetDashboard;

namespace SDM.API.Controllers;

/// <summary>
/// Provides a summary dashboard for the Admin Application.
/// Requires any authenticated admin role.
/// </summary>
public sealed class DashboardController : ApiControllerBase
{
    /// <summary>
    /// Returns a dashboard summary including counts, pending orders, and recent order list.
    /// Restricted to any authenticated admin.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = Policies.RequireAnyAdmin)]
    [ProducesResponseType(typeof(ApiResponse<GetDashboardResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetDashboard(
        [FromQuery] int recentOrdersCount = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new GetDashboardQuery { RecentOrdersCount = recentOrdersCount },
            cancellationToken);
        return HandleResult(result);
    }
}
