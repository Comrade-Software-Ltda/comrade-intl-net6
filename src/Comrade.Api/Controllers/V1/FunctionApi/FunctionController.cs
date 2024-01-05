using System.ComponentModel.DataAnnotations;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Components.FunctionComponent.Queries;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1.FunctionApi;

[FeatureGate(CustomFeature.Alticci)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class FunctionController(IAlticciQuery alticciQuery) : ControllerBase
{
    [HttpGet("alticci/{n:long}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
    public IActionResult Alticci([FromRoute] [Required] long n)
    {
        var result = alticciQuery.CalculaAlticci(n);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
