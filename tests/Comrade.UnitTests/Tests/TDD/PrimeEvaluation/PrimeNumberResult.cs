namespace Comrade.UnitTests.Tests.TDD.PrimeEvaluation;

public class PrimeNumberResult
{
    public PrimeNumberResult(bool isPrime, int iteration)
    {
        IsPrime = isPrime;
        Iteration = iteration;
    }

    public bool IsPrime { get; set; }
    public int Iteration { get; set; }
}
