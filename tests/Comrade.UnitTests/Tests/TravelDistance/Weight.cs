namespace Comrade.UnitTests.Tests.TravelDistance;

public class Weight
{
    public Weight(Node? from, int value)
    {
        From = from;
        Value = value;
    }

    public Node? From { get; }
    public int Value { get; }
}