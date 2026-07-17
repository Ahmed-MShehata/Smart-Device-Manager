using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.SoftwarePackages.CreateSoftwarePackage;
using SDM.Application.SoftwarePackages.DeleteSoftwarePackage;
using SDM.Application.SoftwarePackages.GetSoftwarePackage;
using SDM.Application.SoftwarePackages.GetSoftwarePackages;
using SDM.Application.SoftwarePackages.UpdatePackageStatus;
using SDM.Application.SoftwarePackages.UpdateSoftwarePackage;

namespace SDM.API.Controllers;

/// <summary>
/// Handles all incoming client HTTP requests for software packages.
/// Translates routes into MediatR commands and queries, enforcing policy-based security.
/// </summary>
public sealed class SoftwarePackagesController : ApiControllerBase
{
    /// <summary>
    /// Retrieves a paginated, filtered, and sorted list of software packages.
    /// Accessible anonymously by customers and administrators.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<PaginationResponse<GetSoftwarePackagesResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> GetSoftwarePackages([FromQuery] GetSoftwarePackagesQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves full detailed specifications for a single software package by ID.
    /// Accessible anonymously.
    /// </summary>
    [HttpGet("{id:guid}", Name = nameof(GetSoftwarePackage))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<GetSoftwarePackageResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSoftwarePackage([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new GetSoftwarePackageQuery { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Registers a new software package in the database.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<CreateSoftwarePackageResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateSoftwarePackage([FromBody] CreateSoftwarePackageCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetSoftwarePackage),
                new { id = result.Data.Id },
                new ApiResponse<CreateSoftwarePackageResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Replaces the editable properties of an existing software package.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateSoftwarePackage([FromRoute] Guid id, [FromBody] UpdateSoftwarePackageCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The package ID specified in the URL path does not match the ID in the request body.",
                null,
                [new ApiError("SoftwarePackage.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Updates the availability status of an existing software package.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("{id:guid}/status")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdatePackageStatus([FromRoute] Guid id, [FromBody] UpdatePackageStatusCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The package ID specified in the URL path does not match the ID in the request body.",
                null,
                [new ApiError("SoftwarePackage.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Performs a soft delete by marking the software package's availability status as Deprecated.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteSoftwarePackage([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteSoftwarePackageCommand { Id = id });
        return HandleResult(result);
    }
}
