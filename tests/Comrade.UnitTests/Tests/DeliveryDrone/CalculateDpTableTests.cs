using Comrade.Application.Components.DeliveryOptimizer.Core;
using Comrade.Domain.Models;

namespace Comrade.UnitTests.Tests.DeliveryDrone;

public class CalculateDpTableTests
{
    [Fact]
    public void Test_CalculateDPTable_With_Valid_Inputs()
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

        // Act
        var dpTable = CalculateDPTable.Execute(locations, target);

        // Assert
        Assert.True(dpTable[4, 10]); // Checking if it's possible to get to weight 10 using any of the 4 locations
        Assert.True(dpTable[3, 5]); // Checking if it's possible to get to weight 5 using the first 3 locations
        Assert.False(dpTable[2,
            10]); // Checking if it's impossible to get to weight 10 using only the first 2 locations
    }
}
