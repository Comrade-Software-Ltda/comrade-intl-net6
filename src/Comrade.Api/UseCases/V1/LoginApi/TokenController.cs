#region

using Comrade.Application.Services.AuthenticationServices.Commands;
using Comrade.Application.Services.AuthenticationServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Comrade.Api.UseCases.V1.LoginApi;

[AllowAnonymous]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthenticationCommand _authenticationCommand;

    public TokenController(IAuthenticationCommand authenticationCommand)
    {
        _authenticationCommand = authenticationCommand;
    }


    [HttpPost]
    [Route("generate-token")]
    public async Task<ActionResult> GenerateToken([FromBody] AuthenticationDto dto)
    {
        var result = await _authenticationCommand.GenerateToken(dto).ConfigureAwait(false);

        return Ok(result);
    }
}
