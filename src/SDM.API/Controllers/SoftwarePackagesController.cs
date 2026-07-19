using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.Interfaces;
using SDM.Application.SoftwarePackages.CreateSoftwarePackage;
using SDM.Application.SoftwarePackages.DeleteSoftwarePackage;
using SDM.Application.SoftwarePackages.DownloadSoftwarePackage;
using SDM.Application.SoftwarePackages.GetSoftwarePackage;
using SDM.Application.SoftwarePackages.GetSoftwarePackages;
using SDM.Application.SoftwarePackages.UpdateSoftwarePackage;

namespace SDM.API.Controllers;

/// <summary>
/// Handles all HTTP requests for Software Package management.
/// GET endpoints are anonymous (customer + admin). Write endpoints are admin-only.
/// </summary>
public sealed class SoftwarePackagesController : ApiControllerBase
{
    private readonly IFileStorageService _fileStorage;

    public SoftwarePackagesController(IFileStorageService fileStorage)
    {
        _fileStorage = fileStorage;
    }

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
    /// Downloads the setup file for a software package.
    /// The internal file path is never exposed — the file is streamed directly.
    /// Accessible anonymously.
    /// </summary>
    [HttpGet("{id:guid}/download")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DownloadSoftwarePackage(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DownloadSoftwarePackageQuery { Id = id }, cancellationToken);

        if (!result.IsSuccess)
            return HandleResult(result);

        var absolutePath = _fileStorage.GetAbsolutePath(result.Data!.RelativePath);

        if (absolutePath is null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "The setup file is registered but could not be found on the server. Please contact an administrator.",
                null,
                [new ApiError("SoftwarePackage.FileMissing", "Setup file not found on disk.")]));
        }

        return PhysicalFile(absolutePath, result.Data.ContentType, result.Data.FileName);
    }

    /// <summary>
    /// Creates a new software package entry.
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
    /// Updates an existing software package (metadata and optionally the setup file).
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
    /// Permanently deletes a software package and its associated setup file.
    /// Admin only.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteSoftwarePackage(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteSoftwarePackageCommand { Id = id }, cancellationToken);
        return HandleResult(result);
    }
}
