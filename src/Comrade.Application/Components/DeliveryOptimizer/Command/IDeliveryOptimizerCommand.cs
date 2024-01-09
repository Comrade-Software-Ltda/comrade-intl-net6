using System.IO;
using Microsoft.AspNetCore.Http;

namespace Comrade.Application.Components.DeliveryOptimizer.Command;

public interface IDeliveryOptimizerCommand
{
    MemoryStream Execute(IFormFile file);
}
