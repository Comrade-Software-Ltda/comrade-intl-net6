namespace Comrade.UnitTests.Tests.PrimeNumber;

public static class PrimeEvaluation
{
    public static ResultPrime CheckPrime(int number)
    {
        var iteration = 1;

        if (number < 2)
        {
            return new ResultPrime(false, 0);
        }

        if (number % 2 == 0)
        {
            return new ResultPrime(false, 1);
        }


        var optimizeSearch = Math.Sqrt(number);
        optimizeSearch = Math.Ceiling(optimizeSearch);

        for (var i = 3; i <= optimizeSearch;)
        {
            iteration++;
            if (number % i == 0)
            {
                return new ResultPrime(false, iteration);
            }

            i += 2;
        }

        return new ResultPrime(true, iteration);
    }
}