using System.Linq;
using Comrade.Application.Components.DeliveryOptimizer.Core;
using Comrade.Domain.Models;

namespace Comrade.UnitTests.Tests.DeliveryDrone;

public class GetMinimalCombinationTests
{
    [Fact]
    public void Test_GetMinimalCombination_StandardCase()
    {
        // Arrange
        var drones = new List<Drone>
        {
            new() {Name = "DroneA", MaxWeight = 15},
            new() {Name = "DroneB", MaxWeight = 10}
        };
        var locations = new List<Location>
        {
            new() {Address = "LocationA", PackageWeight = 5},
            new() {Address = "LocationB", PackageWeight = 7},
            new() {Address = "LocationC", PackageWeight = 9}
        };

        // Act
        var deliveries = GetMinimalCombination.Execute(drones, locations);

        // Assert
        Assert.Equal(2, deliveries.Count); // Expecting 2 deliveries
        Assert.Contains(deliveries,
            d => d.AssignedDrone.Name == "DroneA" && d.Locations.Sum(l => l.PackageWeight) == 14);
        Assert.Contains(deliveries,
            d => d.AssignedDrone.Name == "DroneB" && d.Locations.Sum(l => l.PackageWeight) == 7);
    }
}
