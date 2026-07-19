using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.Company.GetCompanyProfile;
using SDM.Application.Company.UpdateCompanyProfile;

namespace SDM.API.Controllers;

/// <summary>
/// Handles HTTP requests for the Company Profile.
/// GET is anonymous — used by the Customer Application Company Information page.
/// PUT requires admin authentication.
/// </summary>
public sealed class CompanyController : ApiControllerBase
{
    /// <summary>
    /// Retrieves the active company profile.
    /// Anonymous — used by the Customer Application.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<CompanyProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetCompanyProfile(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCompanyProfileQuery(), cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Creates or updates the active company profile.
    /// Admin only — used in the Company Information management page.
    /// </summary>
    [HttpPut]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateCompanyProfile(
        [FromBody] UpdateCompanyProfileCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }
}
