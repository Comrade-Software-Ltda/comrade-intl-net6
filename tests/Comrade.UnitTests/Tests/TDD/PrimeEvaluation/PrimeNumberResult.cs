namespace Comrade.UnitTests.Tests.TDD.PrimeEvaluation;

public class PrimeNumberResult(bool isPrime, int iteration)
{
    public bool IsPrime { get; set; } = isPrime;
    public int Iteration { get; set; } = iteration;
}
