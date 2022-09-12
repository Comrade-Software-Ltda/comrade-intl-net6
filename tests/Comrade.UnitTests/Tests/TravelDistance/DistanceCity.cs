namespace Comrade.UnitTests.Tests.TravelDistance;

public class DistanceCity
{
    public DistanceCity(string city1, string city2, int distance)
    {
        City1 = city1;
        City2 = city2;
        Distance = distance;
    }

    public string City1 { get; set; }
    public string City2 { get; set; }
    public int Distance { get; set; }
}