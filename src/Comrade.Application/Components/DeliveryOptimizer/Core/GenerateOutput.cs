using System.IO;
using System.Text;
using Comrade.Domain.Models;

namespace Comrade.Application.Components.DeliveryOptimizer.Core;

public static class GenerateOutput
{
    public static MemoryStream Execute(List<Delivery> optimizedDeliveries, List<Drone> drones)
    {
        var groupedDeliveries = optimizedDeliveries.GroupBy(d => d.AssignedDrone.Name).OrderBy(g => g.Key);

        var result = new StringBuilder();

        foreach (var drone in drones)
        {
            result.AppendLine($"[{drone.Name}]");
            var tripCount = 1;

            var deliveriesByDrone = optimizedDeliveries.Where(x => x.AssignedDrone.Name == drone.Name).ToList();

            foreach (var delivery in deliveriesByDrone)
            {
                result.AppendLine($"Trip #{tripCount}");
                result.AppendLine(string.Join(", ", delivery.Locations.Select(l => $"[{l.Address}]")));
                tripCount++;
            }

            result.AppendLine();
        }

        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result.ToString()));
        return memoryStream;
    }
}
