using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Components.AlticciComponent.Queries;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Comrade.Api.Controllers.V1.AlticciApi;

[FeatureGate(CustomFeature.Airplane)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AlticciController : ControllerBase
{
    private readonly IAlticciQuery _alticciQuery;

    public AlticciController(IAlticciQuery alticciQuery)
    {
        _alticciQuery = alticciQuery;
    }

    [HttpGet("alticci/{n:int}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
    public IActionResult CalcAlticci([FromRoute][Required] int n)
    {
        var result = new AlticciDto(n, _alticciQuery.CalculaAlticci(n));
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
