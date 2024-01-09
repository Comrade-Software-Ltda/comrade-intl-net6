using System.IO;
using Comrade.Application.Components.DeliveryOptimizer.Core;
using Microsoft.AspNetCore.Http;

namespace Comrade.Application.Components.DeliveryOptimizer.Command;

public class DeliveryOptimizerCommand : IDeliveryOptimizerCommand
{
    public MemoryStream Execute(IFormFile file)
    {
        var (drones, locations) = ProcessFile.Execute(file);

        if (drones.Count == 0 || drones.Count > 100 || locations.Count == 0)
        {
            return GenerateErrorOutput.Execute();
        }

        var optimizedDeliveries = GetMinimalCombination.Execute(drones, locations);

        var outputFile = GenerateOutput.Execute(optimizedDeliveries, drones);

        return outputFile;
    }
}
