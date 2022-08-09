using System.ComponentModel.DataAnnotations;
using Comrade.Api.Modules.Common;
using Comrade.Application.Components.FunctionComponent.Queries;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1.FunctionApi;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class FunctionController : ControllerBase
{
    private readonly IAlticciQuery _alticciQuery;

    public FunctionController(IAlticciQuery alticciQuery)
    {
        _alticciQuery = alticciQuery;
    }

    [HttpGet("alticci/{n:long}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
    public IActionResult Alticci([FromRoute][Required] long n)
    {
        var result = _alticciQuery.CalculaAlticci(n);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
