using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.Notifications.CreateNotification;
using SDM.Application.Notifications.DeleteNotification;
using SDM.Application.Notifications.GetNotifications;
using SDM.Application.Notifications.MarkNotificationAsRead;
using SDM.Application.Notifications.ToggleNotificationPin;

namespace SDM.API.Controllers;

/// <summary>
/// Exposes communication delivery functionality and allows admins/customers to review their incoming feeds.
/// </summary>
public sealed class NotificationsController : ApiControllerBase
{
    /// <summary>
    /// Retrieves a paginated feed of notifications structurally scoped to the requesting context via parameters.
    /// Admin context is validated internally.
    /// </summary>
    [HttpGet]
    [Authorize] // Allow all authenticated roles. The query params structure filtering constraints organically.
    [ProducesResponseType(typeof(ApiResponse<PaginationResponse<GetNotificationsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> GetNotifications([FromQuery] GetNotificationsQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    /// <summary>
    /// Issues a new communication block to targeted devices hitting realtime clients implicitly through database sweeps.
    /// Access strictly protected to SuperAdmins and Admins.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<CreateNotificationResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateNotification([FromBody] CreateNotificationCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtAction(
                nameof(GetNotifications),
                null,
                new ApiResponse<CreateNotificationResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Registers a one-way database state transition indicating visual consumption of the designated alert.
    /// Available to Admins resolving tickets or Customers reading their panel alike.
    /// </summary>
    [HttpPut("{id:guid}/read")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> MarkAsRead([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new MarkNotificationAsReadCommand { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Reverses the layout weight properties to force older crucial blocks arbitrarily to the surface.
    /// Access strictly protected to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("{id:guid}/pin")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> TogglePin([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new ToggleNotificationPinCommand { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Performs non-recoverable database obliteration against the historical alert chain.
    /// Access strictly protected to SuperAdmins and Admins.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteNotification([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteNotificationCommand { Id = id });
        return HandleResult(result);
    }
}
