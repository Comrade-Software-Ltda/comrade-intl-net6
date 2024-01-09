using Comrade.Domain.Models;

namespace Comrade.Application.Components.DeliveryOptimizer.Core;

public static class ExtractCombination
{
    public static List<Location> Execute(IReadOnlyList<Location> weights, bool[,] dp, int target)
    {
        var combination = new List<Location>();
        var achievableWeight = target;

        while (achievableWeight > 0 && !dp[weights.Count, achievableWeight])
        {
            achievableWeight--;
        }

        var remainingCapacity = achievableWeight;

        for (var i = weights.Count; i > 0 && remainingCapacity > 0; i--)
        {
            if (!dp[i - 1, remainingCapacity] && remainingCapacity - weights[i - 1].PackageWeight >= 0 &&
                dp[i - 1, remainingCapacity - weights[i - 1].PackageWeight])
            {
                combination.Add(weights[i - 1]);
                remainingCapacity -= weights[i - 1].PackageWeight;
            }
        }

        return combination;
    }
}
