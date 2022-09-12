namespace Comrade.UnitTests.Tests.TravelDistance;

public class GpsTravel
{
    public GpsTravel()
    {
        DistanceCities = new List<DistanceCity>();
        CityNames = new List<string>();
        CityFrom = "";
        CityTo = "";
    }

    public GpsTravel(int city, List<string> cityNames, int road, List<DistanceCity> distanceCities, string cityFrom,
        string cityTo)
    {
        City = city;
        CityNames = cityNames;
        Road = road;
        DistanceCities = distanceCities;
        CityFrom = cityFrom;
        CityTo = cityTo;
    }

    public int City { get; set; }
    public List<string> CityNames { get; set; }
    public int Road { get; set; }
    public List<DistanceCity> DistanceCities { get; set; }
    public string CityFrom { get; set; }
    public string CityTo { get; set; }
}