using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.AdminUsers.LoginAdminUser;

namespace SDM.API.Controllers;

/// <summary>
/// Exposes public endpoints for asserting identity and generating authorization sessions.
/// </summary>
public sealed class AuthController : ApiControllerBase
{
    /// <summary>
    /// Authenticates an administrator using their unique username and password.
    /// Returns a signed JSON Web Token (JWT) on success.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<LoginAdminUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Login([FromBody] LoginAdminUserCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
