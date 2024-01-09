namespace Comrade.UnitTests.Tests.TDD.PrimeEvaluation;

public static class PrimeNumberEvaluation
{
    public static PrimeNumberResult CheckIfIsPrimeNumber(int number)
    {
        var iteration = 1;

        if (number < 2)
        {
            return new PrimeNumberResult(false, 0);
        }

        if (number % 2 == 0)
        {
            return new PrimeNumberResult(false, 1);
        }


        var optimizeSearch = Math.Sqrt(number);
        optimizeSearch = Math.Ceiling(optimizeSearch);

        for (var i = 3; i <= optimizeSearch; i += 2)
        {
            iteration++;
            if (number % i == 0)
            {
                return new PrimeNumberResult(false, iteration);
            }
        }

        return new PrimeNumberResult(true, iteration);
    }
}
