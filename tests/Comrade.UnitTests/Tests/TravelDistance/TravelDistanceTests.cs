using Xunit;
using Xunit.Abstractions;

namespace Comrade.UnitTests.Tests.TravelDistance;

public class TravelDistanceTests
{
    private readonly ITestOutputHelper _output;
    private readonly GpsFitSolution _gpsFitSolution;

    public TravelDistanceTests(ITestOutputHelper output)
    {
        _output = output;
        _gpsFitSolution = new GpsFitSolution();
    }

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