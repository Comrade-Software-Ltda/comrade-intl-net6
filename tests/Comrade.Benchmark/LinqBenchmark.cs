using BenchmarkDotNet.Attributes;

namespace Comrade.Benchmark;

public class LinqBenchmark
{
    private const int capacity = 1000;
    private const int slice = 500;
    private readonly int[] numbers = new int[capacity];

    [GlobalSetup]
    public void Setup()
    {
        for (var i = 0; i < capacity; i++)
        {
            numbers[i] = i;
        }
    }

    [Benchmark]
    public int LinqWhereAndFirst()
    {
        return numbers.Where(x => x > slice).First();
    }

    [Benchmark]
    public int LinqFirst()
    {
        return numbers.First(x => x > slice);
    }

    [Benchmark(Baseline = true)]
    public int NoLinq()
    {
        for (var i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] > slice)
            {
                return numbers[i];
            }
        }

        throw new InvalidOperationException();
    }
}
