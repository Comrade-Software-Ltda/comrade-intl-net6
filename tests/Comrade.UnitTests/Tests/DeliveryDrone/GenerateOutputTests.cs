using Comrade.Application.Components.DeliveryOptimizer.Core;

namespace Comrade.UnitTests.Tests.DeliveryDrone;

public class GenerateOutputTests
{
    [Fact]
    public async Task Test_GenerateOutput_ValidInput()
    {
        // Arrange
        var droneSimulatorFile = await GetIFormFileDroneMock.Execute();

        var (drones, locations) = ProcessFile.Execute(droneSimulatorFile);
        var deliveries = GetMinimalCombination.Execute(drones, locations);


        // Act
        var memoryStream = GenerateOutput.Execute(deliveries, drones);
        var reader = new StreamReader(memoryStream);
        var result = reader.ReadToEnd();

        // Assert
        var expectedOutput =
            "[DroneA]\r\n\r\n[DroneB]\r\nTrip #1\r\n[LocationC], [LocationA]\r\nTrip #2\r\n[LocationE], [LocationB]\r\nTrip #3\r\n[LocationG], [LocationF]\r\nTrip #4\r\n[LocationK], [LocationI], [LocationD]\r\nTrip #5\r\n[LocationO], [LocationN], [LocationM], [LocationL], [LocationJ], [LocationH]\r\n\r\n[DroneC]\r\nTrip #1\r\n[LocationP]\r\n\r\n";

        Assert.Equal(expectedOutput, result);
    }

    // Add more tests for various scenarios and edge cases.
}
