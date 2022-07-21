using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Components.AlticciComponent.Queries;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Comrade.Api.Controllers.V1.AlticciApi;

// [Authorize]
[FeatureGate(CustomFeature.Airplane)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AlticciController : ControllerBase
{
    private readonly IAlticciQuery _alticciQuery;
    private readonly ILogger<AlticciController> _logger;

    public AlticciController(IAlticciQuery alticciQuery, ILogger<AlticciController> logger)
    {
        _alticciQuery = alticciQuery;
        _logger = logger;
    }

    [HttpGet("alticci/{n:int}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
    public IActionResult CalcAlticci([FromRoute][Required] int n)
    {
        var result = _alticciQuery.CalculaAlticci(n);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
