using Comrade.Domain.Models;

namespace Comrade.Application.Components.DeliveryOptimizer.Core;

public static class CalculateDPTable
{
    public static bool[,] Execute(List<Location> weights, int target)
    {
        var dp = new bool[weights.Count + 1, target + 1];

        dp[0, 0] = true;

        for (var itemIndex = 1; itemIndex <= weights.Count; itemIndex++)
        {
            for (var weightIndex = 0; weightIndex <= target; weightIndex++)
            {
                var currentPackageWeight = weights[itemIndex - 1].PackageWeight;

                var isPossibleWithoutCurrent = dp[itemIndex - 1, weightIndex];
                var isPossibleWithCurrent = currentPackageWeight <= weightIndex
                                            && dp[itemIndex - 1, weightIndex - currentPackageWeight];

                dp[itemIndex, weightIndex] = isPossibleWithoutCurrent || isPossibleWithCurrent;
            }
        }

        return dp;
    }
}
