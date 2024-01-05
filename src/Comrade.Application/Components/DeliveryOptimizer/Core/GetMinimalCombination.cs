using Comrade.Domain.Models;

namespace Comrade.Application.Components.DeliveryOptimizer.Core;

public static class GetMinimalCombination
{
    public static List<Delivery> Execute(List<Drone> drones, List<Location> locations)
    {
        var optimizedDeliveries = new List<Delivery>();

        var target = drones.Max(x => x.MaxWeight);

        while (locations.Count > 0)
        {
            var dp = CalculateDPTable.Execute(locations, target);

            var combination = ExtractCombination.Execute(locations, dp, target);

            var combinationWeight = combination.Sum(x => x.PackageWeight);

            var assignedDrone = drones
                .Where(n => n.MaxWeight >= combinationWeight)
                .Min();


            optimizedDeliveries.Add(new
                Delivery
                {
                    Locations = combination,
                    AssignedDrone = assignedDrone
                });

            foreach (var item in combination)
            {
                locations.Remove(item);
            }
        }

        return optimizedDeliveries;
    }
}
