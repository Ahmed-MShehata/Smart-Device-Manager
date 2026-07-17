using MediatR;
using Microsoft.AspNetCore.Mvc;
using SDM.Application.Common;

namespace SDM.API.Controllers;

/// <summary>
/// Abstract base controller for API endpoints.
/// Encapsulates MediatR sender access and maps Application layer <see cref="Result"/>
/// objects into standard HTTP responses following the SDM API design guidelines.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    /// <summary>
    /// Gets the MediatR sender instance resolved from HTTP context request services.
    /// </summary>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    /// <summary>
    /// Maps a typed <see cref="Result{T}"/> into a standard HTTP Ok response or detailed failure.
    /// </summary>
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(new ApiResponse<T>(true, result.Message, result.Data, []));
        }

        return MapFailure(result);
    }

    /// <summary>
    /// Maps a non-generic <see cref="Result"/> into a standard HTTP Ok response or detailed failure.
    /// </summary>
    protected ActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
        {
            return Ok(new ApiResponse<object>(true, result.Message, null, []));
        }

        return MapFailure(result);
    }

    private ActionResult MapFailure(Result result)
    {
        var errors = result.Errors.Select(e => new ApiError(e.Code, e.Description)).ToList();
        var firstError = result.Errors.FirstOrDefault();

        if (firstError == null)
        {
            return BadRequest(new ApiResponse<object>(false, result.Message, null, []));
        }

        // Map well-known domain/validation error codes to corresponding HTTP status codes
        if (firstError.Code.EndsWith(".NotFound"))
        {
            return NotFound(new ApiResponse<object>(false, result.Message, null, errors));
        }

        if (firstError.Code.EndsWith(".Conflict"))
        {
            return Conflict(new ApiResponse<object>(false, result.Message, null, errors));
        }

        if (firstError.Code == "Auth.Unauthorized")
        {
            return Unauthorized(new ApiResponse<object>(false, result.Message, null, errors));
        }

        if (firstError.Code == "Auth.Forbidden")
        {
            return StatusCode(StatusCodes.Status403Forbidden,
                new ApiResponse<object>(false, result.Message, null, errors));
        }

        if (firstError.Code.StartsWith("Validation."))
        {
            return UnprocessableEntity(new ApiResponse<object>(false, result.Message, null, errors));
        }

        return BadRequest(new ApiResponse<object>(false, result.Message, null, errors));
    }
}

/// <summary>
/// Unified API response wrapper conforming to the SDM dynamic API specifications.
/// </summary>
public record ApiResponse<T>(
    bool Success,
    string Message,
    T? Data,
    IReadOnlyList<ApiError> Errors);

/// <summary>
/// Represents a structured API serialization error returned in the response payload.
/// </summary>
public record ApiError(
    string Code,
    string Description);
