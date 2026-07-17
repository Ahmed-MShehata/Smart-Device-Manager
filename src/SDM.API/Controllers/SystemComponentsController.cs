using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.SystemComponents.CreateSystemComponent;
using SDM.Application.SystemComponents.DeleteSystemComponent;
using SDM.Application.SystemComponents.GetSystemComponent;
using SDM.Application.SystemComponents.GetSystemComponents;
using SDM.Application.SystemComponents.UpdateComponentStatus;
using SDM.Application.SystemComponents.UpdateSystemComponent;

namespace SDM.API.Controllers;

/// <summary>
/// Handles all incoming client HTTP requests for system components.
/// Translates routes into MediatR commands and queries, enforcing policy-based security.
/// </summary>
public sealed class SystemComponentsController : ApiControllerBase
{
    /// <summary>
    /// Retrieves a paginated, filtered, and sorted list of system components.
    /// Accessible anonymously by customers and administrators.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<PaginationResponse<GetSystemComponentsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> GetSystemComponents([FromQuery] GetSystemComponentsQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves full detailed specifications for a single system component by ID.
    /// Accessible anonymously.
    /// </summary>
    [HttpGet("{id:guid}", Name = nameof(GetSystemComponent))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<GetSystemComponentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSystemComponent([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new GetSystemComponentQuery { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Registers a new system component in the database.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<CreateSystemComponentResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateSystemComponent([FromBody] CreateSystemComponentCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetSystemComponent),
                new { id = result.Data.Id },
                new ApiResponse<CreateSystemComponentResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Replaces the editable properties of an existing system component.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateSystemComponent([FromRoute] Guid id, [FromBody] UpdateSystemComponentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The component ID specified in the URL path does not match the ID in the request body.",
                null,
                [new ApiError("SystemComponent.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Updates the availability status of an existing system component.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("{id:guid}/status")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateComponentStatus([FromRoute] Guid id, [FromBody] UpdateComponentStatusCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The component ID specified in the URL path does not match the ID in the request body.",
                null,
                [new ApiError("SystemComponent.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Performs a soft delete by marking the system component's availability status as Deprecated.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteSystemComponent([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteSystemComponentCommand { Id = id });
        return HandleResult(result);
    }
}
