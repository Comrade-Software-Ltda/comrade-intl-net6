using Comrade.UnitTests.Tests.TDD.PrimeEvaluation.TestDatas;

namespace Comrade.UnitTests.Tests.TDD.PrimeEvaluation;

public class PrimeNumberTests
{
    [Theory]
    [ClassData(typeof(PrimeTestData))]
    public void IsPrimeTest(int inputValue, PrimeNumberResult expectedResult)
    {
        var result = PrimeNumberEvaluation.CheckIfIsPrimeNumber(inputValue);

        Assert.Equal(expectedResult.IsPrime, result.IsPrime);
        Assert.Equal(expectedResult.Iteration, result.Iteration);
    }

    [Fact]
    public void NotPrimeTest()
    {
        var result = PrimeNumberEvaluation.CheckIfIsPrimeNumber(999337);

        Assert.False(result.IsPrime);
        Assert.Equal(117, result.Iteration);
    }
}
