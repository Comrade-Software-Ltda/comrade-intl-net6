namespace Comrade.UnitTests.Tests.PrimeNumber;

public class ResultPrime
{
    public ResultPrime(bool isPrime, int iteration)
    {
        IsPrime = isPrime;
        Iteration = iteration;
    }

    public bool IsPrime { get; set; }
    public int Iteration { get; set; }
}