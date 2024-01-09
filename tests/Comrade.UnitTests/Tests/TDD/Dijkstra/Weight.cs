namespace Comrade.UnitTests.Tests.TDD.Dijkstra;

public class Weight(Node? from, int value)
{
    public Node? From { get; } = from;
    public int Value { get; } = value;
}
