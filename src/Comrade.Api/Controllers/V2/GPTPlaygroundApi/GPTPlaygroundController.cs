using Comrade.Api.Bases;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using Comrade.Application.Components.Airplane.Commands;
using Comrade.Application.Components.GPTPlayground.Commands;

namespace Comrade.Api.Controllers.V2.GPTPlaygroundApi;

// [Authorize]
[FeatureGate(CustomFeature.GPTPlayground)]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class GPTPlaygroundController(
    IGPTPlaygroundCommand gPTPlaygroundCommand,
    ILogger<GPTPlaygroundController> logger)
    : ComradeController
{
    private readonly ILogger<GPTPlaygroundController> _logger = logger;

    [HttpGet("1")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var oto = await gPTPlaygroundCommand.Create();
            return Ok(oto);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }


}
