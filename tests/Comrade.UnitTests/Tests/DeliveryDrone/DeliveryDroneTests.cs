using System.Diagnostics.CodeAnalysis;
using System.Text;
using Comrade.Application.Components.DeliveryOptimizer.Command;

namespace Comrade.UnitTests.Tests.DeliveryDrone;

[ExcludeFromCodeCoverage]
public class DeliveryDroneTests
{
    [Fact]
    public async Task Should_Process_Correct_File_And_Return_Expected_Output()
    {
        // Arrange
        var droneSimulatorFile = await GetIFormFileDroneMock.Execute();

        var deliveryOptimizerCommand = new DeliveryOptimizerCommand();

        var input =
            "[DroneA]\r\n\r\n[DroneB]\r\nTrip #1\r\n[LocationC], [LocationA]\r\nTrip #2\r\n[LocationE], [LocationB]\r\nTrip #3\r\n[LocationG], [LocationF]\r\nTrip #4\r\n[LocationK], [LocationI], [LocationD]\r\nTrip #5\r\n[LocationO], [LocationN], [LocationM], [LocationL], [LocationJ], [LocationH]\r\n\r\n[DroneC]\r\nTrip #1\r\n[LocationP]\r\n\r\n";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));


        // Act
        var result = deliveryOptimizerCommand.Execute(droneSimulatorFile);
        using var srExpected = new StreamReader(inputStream);
        using var srActual = new StreamReader(result);

        var strExpected = srExpected.ReadToEnd();
        var strActual = srActual.ReadToEnd();

        // Assert
        Assert.Equal(strExpected, strActual);
    }

    [Fact]
    public async Task Should_Process_Different_Input_File_And_Return_Expected_Output()
    {
        // Arrange
        var droneSimulatorFile = await GetIFormFileDroneMock.ExecuteDifferentFile();

        var deliveryOptimizerCommand = new DeliveryOptimizerCommand();

        var input =
            "[DroneA]\r\nTrip #1\r\n[LocationF]\r\nTrip #2\r\n[LocationR], [LocationQ]\r\n\r\n[DroneB]\r\nTrip #1\r\n[LocationG], [LocationD], [LocationC]\r\nTrip #2\r\n[LocationJ], [LocationH], [LocationE]\r\nTrip #3\r\n[LocationM], [LocationL], [LocationB]\r\nTrip #4\r\n[LocationP], [LocationN], [LocationK], [LocationI]\r\nTrip #5\r\n[LocationO], [LocationA]\r\n\r\n[DroneC]\r\nTrip #1\r\n[LocationS]\r\n\r\n";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));

        // Act
        var result = deliveryOptimizerCommand.Execute(droneSimulatorFile);
        using var srExpected = new StreamReader(inputStream);
        using var srActual = new StreamReader(result);

        var strExpected = srExpected.ReadToEnd();
        var strActual = srActual.ReadToEnd();

        // Assert
        Assert.Equal(strExpected, strActual);
    }

    [Fact]
    public async Task Should_Return_Error_For_Incorrect_File_Format()
    {
        // Arrange
        var droneSimulatorFile = await GetIFormFileDroneMock.ExecuteWrongFile();

        var deliveryOptimizerCommand = new DeliveryOptimizerCommand();

        var input =
            "Invalid input. Please check the data and try again.\r\n";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));

        // Act
        var result = deliveryOptimizerCommand.Execute(droneSimulatorFile);
        using var srExpected = new StreamReader(inputStream);
        using var srActual = new StreamReader(result);

        var strExpected = srExpected.ReadToEnd();
        var strActual = srActual.ReadToEnd();

        // Assert
        Assert.Equal(strExpected, strActual);
    }

    [Fact]
    public async Task Should_Return_Error_For_File_With_Invalid_Data()
    {
        // Arrange
        var droneSimulatorFile = await GetIFormFileDroneMock.ExecuteFileWithError();

        var deliveryOptimizerCommand = new DeliveryOptimizerCommand();

        var input =
            "Invalid input. Please check the data and try again.\r\n";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));

        // Act
        var result = deliveryOptimizerCommand.Execute(droneSimulatorFile);
        using var srExpected = new StreamReader(inputStream);
        using var srActual = new StreamReader(result);

        var strExpected = srExpected.ReadToEnd();
        var strActual = srActual.ReadToEnd();

        // Assert
        Assert.Equal(strExpected, strActual);
    }

    [Fact]
    public void Should_Return_Error_For_Empty_Input_File()
    {
        // Arrange
        var droneSimulatorFile = GetIFormFileDroneMock.ExecuteEmptyFile();
        var deliveryOptimizerCommand = new DeliveryOptimizerCommand();

        var input =
            "Invalid input. Please check the data and try again.\r\n";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));

        // Act
        var result = deliveryOptimizerCommand.Execute(droneSimulatorFile);
        using var srExpected = new StreamReader(inputStream);
        using var srActual = new StreamReader(result);

        var strExpected = srExpected.ReadToEnd();
        var strActual = srActual.ReadToEnd();

        // Assert
        Assert.Equal(strExpected, strActual);
    }
}
