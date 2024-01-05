using System.Diagnostics.CodeAnalysis;
using Comrade.Application.Components.DeliveryOptimizer.Core;

namespace Comrade.UnitTests.Tests.DeliveryDrone;

[ExcludeFromCodeCoverage]
public class ProcessFileTests
{
    [Fact]
    public async Task Should_Return_Expected_Attack_Location()
    {
        // Arrange
        var droneSimulatorFile = await GetIFormFileDroneMock.Execute();

        // Act
        var (drones, locations) = ProcessFile.Execute(droneSimulatorFile);

        // Assert
        Assert.Equal(3, drones.Count);
        Assert.Equal("DroneA", drones[0].Name);
        Assert.Equal(200, drones[0].MaxWeight);
        Assert.Equal("DroneB", drones[1].Name);
        Assert.Equal(250, drones[1].MaxWeight);

        Assert.Equal(16, locations.Count);
        Assert.Equal("LocationA", locations[0].Address);
        Assert.Equal(200, locations[0].PackageWeight);
        Assert.Equal("LocationB", locations[1].Address);
        Assert.Equal(150, locations[1].PackageWeight);
    }
}
