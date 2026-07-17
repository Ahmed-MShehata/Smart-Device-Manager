using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.Settings.CreateSetting;
using SDM.Application.Settings.DeleteSetting;
using SDM.Application.Settings.GetPublicSettings;
using SDM.Application.Settings.GetSetting;
using SDM.Application.Settings.GetSettings;
using SDM.Application.Settings.UpdateSetting;

namespace SDM.API.Controllers;

/// <summary>
/// Handles all incoming client HTTP requests for global system settings.
/// Publicly exposed settings can be retrieved anonymously.
/// Administrative modifications require proper authorization.
/// </summary>
public sealed class SettingsController : ApiControllerBase
{
    /// <summary>
    /// Retrieves a dictionary-like list of settings explicitly marked as public.
    /// Accessible anonymously by customer applications to retrieve runtime configuration.
    /// </summary>
    [HttpGet("public")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetPublicSettingsResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetPublicSettings()
    {
        var result = await Mediator.Send(new GetPublicSettingsQuery());
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves a paginated, filtered, and sorted list of all settings.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<PaginationResponse<GetSettingsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> GetSettings([FromQuery] GetSettingsQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves full detailed specifications for a single setting by ID.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpGet("{id:guid}", Name = nameof(GetSetting))]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<GetSettingResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSetting([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new GetSettingQuery { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Registers a new global setting in the database.
    /// Restricted to SuperAdmins.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Policies.RequireSuperAdmin)]
    [ProducesResponseType(typeof(ApiResponse<CreateSettingResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateSetting([FromBody] CreateSettingCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetSetting),
                new { id = result.Data.Id },
                new ApiResponse<CreateSettingResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Replaces the properties of an existing configuration setting.
    /// Restricted to SuperAdmins.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.RequireSuperAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateSetting([FromRoute] Guid id, [FromBody] UpdateSettingCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The setting ID specified in the URL path does not match the ID in the request body.",
                null,
                [new ApiError("Setting.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Performs a hard delete permanently removing the specified setting.
    /// Restricted to SuperAdmins.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.RequireSuperAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteSetting([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteSettingCommand { Id = id });
        return HandleResult(result);
    }
}
