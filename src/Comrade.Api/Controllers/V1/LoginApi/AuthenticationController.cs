using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Components.Authentication.Commands;
using Comrade.Application.Components.Authentication.Contracts;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1.LoginApi;

[FeatureGate(CustomFeature.Authentication)]
[AllowAnonymous]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthenticationController(IAuthenticationCommand authenticationCommand) : ControllerBase
{
    [HttpPost("update-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>),
        StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdatePassword([FromBody] AuthenticationDto dto)
    {
        try
        {
            var result = await authenticationCommand.UpdatePassword(dto);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>),
        StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> ForgotPassword([FromBody] AuthenticationDto dto)
    {
        try
        {
            var result = await authenticationCommand.ForgotPassword(dto);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}
