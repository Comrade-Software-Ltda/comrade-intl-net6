using Comrade.Api.Modules.Common;
using Comrade.Application.Components.DeliveryOptimizer.Command;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class DeliveryDroneController : ControllerBase
{
    private readonly IDeliveryOptimizerCommand _deliveryOptimizerCommand;
    private readonly ILogger<DeliveryDroneController> _logger;

    public DeliveryDroneController(ILogger<DeliveryDroneController> logger,
        IDeliveryOptimizerCommand deliveryOptimizerCommand)
    {
        _logger = logger;
        _deliveryOptimizerCommand = deliveryOptimizerCommand;
    }

    [HttpPost("delivery-optimizer")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Post))]
    public IActionResult DownloadDeliveryReport(IFormFile file)
    {
        try
        {
            var result = _deliveryOptimizerCommand.Execute(file);

            return File(result, "text/plain", "deliveryReport.txt");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ex);
        }
    }
}
