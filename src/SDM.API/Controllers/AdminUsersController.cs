using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.AdminUsers.CreateAdminUser;
using SDM.Application.AdminUsers.UpdateAdminStatus;

namespace SDM.API.Controllers;

/// <summary>
/// Exposes endpoints for managing internal staff access and hierarchy.
/// Access is strictly limited to SuperAdmins.
/// </summary>
public sealed class AdminUsersController : ApiControllerBase
{
    /// <summary>
    /// Provisions a new administrative user account with a secure initial password.
    /// Restricted to SuperAdmins.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Policies.RequireSuperAdmin)]
    [ProducesResponseType(typeof(ApiResponse<CreateAdminUserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateAdminUser([FromBody] CreateAdminUserCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            // We omit the GET route since there is no GetAdminUser slice built yet,
            // returning a 201 Created without a Location header.
            return Created(
                uri: string.Empty,
                value: new ApiResponse<CreateAdminUserResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Enables or disables an administrator account, prohibiting logins when disabled.
    /// Restricted to SuperAdmins.
    /// </summary>
    [HttpPut("{id:guid}/status")]
    [Authorize(Policy = Policies.RequireSuperAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAdminStatus([FromRoute] Guid id, [FromBody] UpdateAdminStatusCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The admin user ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("AdminUser.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
