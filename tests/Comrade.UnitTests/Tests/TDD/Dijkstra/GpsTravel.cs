namespace Comrade.UnitTests.Tests.TDD.Dijkstra;

public class GpsTravel(
    int city,
    List<string> cityNames,
    int road,
    List<DistanceCity> distanceCities,
    string cityFrom,
    string cityTo)
{
    public GpsTravel() : this(0, new List<string>(), 0, new List<DistanceCity>(), "", "")
    {
    }

    public int City { get; set; } = city;
    public List<string> CityNames { get; set; } = cityNames;
    public int Road { get; set; } = road;
    public List<DistanceCity> DistanceCities { get; set; } = distanceCities;
    public string CityFrom { get; set; } = cityFrom;
    public string CityTo { get; set; } = cityTo;
}
