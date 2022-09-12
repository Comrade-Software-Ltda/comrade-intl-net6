using Xunit;

namespace Comrade.UnitTests.Tests.PrimeNumber;

public class PrimeNumberTests
{
    [Fact]
    public void IsPrimeTest()
    {
        var result = PrimeEvaluation.CheckPrime(999331);

        Assert.True(result.IsPrime);
        Assert.Equal(500, result.Iteration);
    }

    [Fact]
    public void NotPrimeTest()
    {
        var result = PrimeEvaluation.CheckPrime(999337);

        Assert.False(result.IsPrime);
        Assert.Equal(117, result.Iteration);
    }
}