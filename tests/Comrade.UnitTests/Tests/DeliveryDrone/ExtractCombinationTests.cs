using Comrade.Application.Components.DeliveryOptimizer.Core;
using Comrade.Domain.Models;

namespace Comrade.UnitTests.Tests.DeliveryDrone;

public class ExtractCombinationTests
{
    [Fact]
    public void Test_ExtractCombination_ValidDPTable()
    {
        // Arrange
        var locations = new List<Location>
        {
            new() {PackageWeight = 2},
            new() {PackageWeight = 3},
            new() {PackageWeight = 7},
            new() {PackageWeight = 8}
        };
        var target = 10;
        var dp = CalculateDPTable.Execute(locations, target);

        // Act
        var combination = ExtractCombination.Execute(locations, dp, target);

        // Assert
        Assert.Contains(locations[1], combination); // Expecting location with weight 3
        Assert.Contains(locations[2], combination); // Expecting location with weight 7
        Assert.DoesNotContain(locations[3], combination); // Not expecting location with weight 8
    }

    [Fact]
    public void Test_ExtractCombination_InvalidDPTable()
    {
        // Arrange
        var locations = new List<Location>
        {
            new() {PackageWeight = 10},
            new() {PackageWeight = 15},
            new() {PackageWeight = 20}
        };
        var target = 5;
        var dp = new bool[locations.Count + 1, target + 1]; // Empty DP table

        // Act
        var combination = ExtractCombination.Execute(locations, dp, target);

        // Assert
        Assert.Empty(combination); // Expecting no locations as none of them fit within the target weight
    }
}
