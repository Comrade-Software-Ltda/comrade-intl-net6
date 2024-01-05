using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Components.AuthenticationComponent.Commands;
using Comrade.Application.Components.AuthenticationComponent.Contracts;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1.LoginApi;

[FeatureGate(CustomFeature.Authentication)]
[AllowAnonymous]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class TokenController(IAuthenticationCommand authenticationCommand) : ControllerBase
{
    [HttpPost("generate-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>),
        StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GenerateToken([FromBody] AuthenticationDto dto)
    {
        try
        {
            var result = await authenticationCommand.GenerateToken(dto);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}
