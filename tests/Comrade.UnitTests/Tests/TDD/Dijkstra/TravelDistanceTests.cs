using Xunit;
using Xunit.Abstractions;

namespace Comrade.UnitTests.Tests.TDD.Dijkstra;

public class TravelDistanceTests(ITestOutputHelper output)
{
    private readonly GpsFitSolution _gpsFitSolution = new();
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void TravelDistance()
    {
        var result = _gpsFitSolution.Result(@"C:\Users\Jhonatan\Downloads\EntradaGPS.txt");

        Assert.Equal(3, result.Count);
        Assert.Equal(3, result[0]);
        Assert.Equal(1, result[1]);
        Assert.Equal(2, result[2]);
    }
}
