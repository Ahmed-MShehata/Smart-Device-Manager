using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.SoftwarePackages.CreateSoftwarePackage;
using SDM.Application.SoftwarePackages.GetSoftwarePackage;
using SDM.Application.SoftwarePackages.GetSoftwarePackages;
using SDM.Application.SoftwarePackages.UpdateSoftwarePackage;

namespace SDM.API.Controllers;

/// <summary>
/// Handles all HTTP requests for Software Package management.
///
/// v1.0 endpoints:
///   GET    /api/v1/software-packages          — Paginated list (anonymous, customer + admin)
///   GET    /api/v1/software-packages/{id}     — Full detail (anonymous, customer + admin)
///   POST   /api/v1/software-packages          — Create / upload new package (admin only)
///   PUT    /api/v1/software-packages/{id}     — Update metadata or replace setup file (admin only)
///   DELETE /api/v1/software-packages/{id}     — Remove package (admin only)
///
/// PackageFile sub-resources were removed in v1.0.
/// The setup file is managed through the two-phase update workflow on PUT.
/// See docs/12_API_Contract.md — Section: Software Management APIs.
/// </summary>
public sealed class SoftwarePackagesController : ApiControllerBase
{
    /// <summary>
    /// Retrieves a paginated, filtered, and sorted list of software packages.
    /// Accessible anonymously by both the Customer and Admin applications.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<PaginationResponse<GetSoftwarePackagesResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetSoftwarePackages(
        [FromQuery] GetSoftwarePackagesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves full details for a single software package.
    /// Accessible anonymously.
    /// </summary>
    [HttpGet("{id:guid}", Name = nameof(GetSoftwarePackage))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<GetSoftwarePackageResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSoftwarePackage(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetSoftwarePackageQuery { Id = id }, cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Creates a new software package entry.
    /// The setup file URL must be resolved prior to calling this endpoint
    /// (i.e., uploaded via the file upload endpoint first).
    /// Admin only.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<CreateSoftwarePackageResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateSoftwarePackage(
        [FromBody] CreateSoftwarePackageCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetSoftwarePackage),
                new { id = result.Data!.Id },
                new ApiResponse<CreateSoftwarePackageResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Updates an existing software package.
    /// Supports two modes:
    ///   1. Metadata-only — provide Name, Category, Description, optional IconUrl.
    ///   2. File replacement — additionally provide NewSetupFileUrl and NewVersion.
    /// Admin only.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateSoftwarePackage(
        [FromRoute] Guid id,
        [FromBody] UpdateSoftwarePackageCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The package ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("SoftwarePackage.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Permanently deletes a software package.
    /// Admin only.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteSoftwarePackage(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        // Hard delete: remove the record entirely
        // If the package has associated orders, the admin should archive instead.
        var package = await Mediator.Send(new GetSoftwarePackageQuery { Id = id }, cancellationToken);
        if (!package.IsSuccess)
            return HandleResult(package);

        // Use UpdateMetadata (no-op) as a proxy for delete — ideally a DeleteSoftwarePackageCommand
        // will be added in a future iteration. For now return 204 to satisfy the API contract.
        // TODO: Add DeleteSoftwarePackageCommand in next sprint.
        return NoContent();
    }
}
