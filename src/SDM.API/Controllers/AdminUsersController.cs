using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.AdminUsers.CreateAdminUser;
using SDM.Application.AdminUsers.DeleteAdminUser;
using SDM.Application.AdminUsers.GetAdminUser;
using SDM.Application.AdminUsers.GetAdminUsers;
using SDM.Application.AdminUsers.UpdateAdminStatus;
using SDM.Application.AdminUsers.UpdateAdminUser;
using SDM.Application.Common;

namespace SDM.API.Controllers;

/// <summary>
/// Exposes endpoints for managing internal staff access and hierarchy.
/// All actions are strictly limited to SuperAdmins.
/// </summary>
[Authorize(Policy = Policies.RequireSuperAdmin)]
public sealed class AdminUsersController : ApiControllerBase
{
    /// <summary>
    /// Retrieves a paginated, searchable list of all admin accounts.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PaginationResponse<GetAdminUsersResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAdminUsers(
        [FromQuery] GetAdminUsersQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves the full detail of a single admin account by its ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = nameof(GetAdminUser))]
    [ProducesResponseType(typeof(ApiResponse<GetAdminUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAdminUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAdminUserQuery { Id = id }, cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Provisions a new administrative user account with a secure initial password.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateAdminUserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateAdminUser([FromBody] CreateAdminUserCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetAdminUser),
                new { id = result.Data!.Id },
                new ApiResponse<CreateAdminUserResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Updates the username, role, and optionally the password of an existing admin account.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateAdminUser(
        [FromRoute] Guid id,
        [FromBody] UpdateAdminUserCommand command)
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

    /// <summary>
    /// Enables or disables an administrator account, prohibiting logins when disabled.
    /// </summary>
    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAdminStatus(
        [FromRoute] Guid id,
        [FromBody] UpdateAdminStatusCommand command)
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

    /// <summary>
    /// Soft-deletes an admin account by deactivating it.
    /// The record is never physically removed from the database.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> DeleteAdminUser([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteAdminUserCommand { Id = id });
        return HandleResult(result);
    }
}
