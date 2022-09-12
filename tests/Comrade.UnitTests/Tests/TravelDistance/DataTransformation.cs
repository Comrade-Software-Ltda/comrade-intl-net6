using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace Comrade.UnitTests.Tests.TravelDistance;

public static class DataTransformation
{
    public static List<GpsTravel> ExtractDataFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath).ToList();

        int.TryParse(lines[0], out var numberOfTests);


        var gpsTravels = new List<GpsTravel>();

        var iterator = 1;

        for (var i = 0; i < numberOfTests; i++)
        {
            var gpsTravel = new GpsTravel();
            int.TryParse(lines[iterator], out var city);
            iterator++;
            gpsTravel.City = city;

            gpsTravel.CityNames = lines[iterator].Split(" ").Where(x => !x.IsNullOrEmpty()).ToList();
            iterator++;

            int.TryParse(lines[iterator], out var road);
            iterator++;

            gpsTravel.Road = road;

            for (var j = 0; j < gpsTravel.Road; j++)
            {
                var distances = lines[iterator + j].Split(" ").ToList();
                int.TryParse(distances[2], out var distanceBetween);

                var tes = new DistanceCity(distances[0], distances[1], distanceBetween);
                gpsTravel.DistanceCities.Add(tes);
            }

            iterator += road;

            var path = lines[iterator].Split(" ").ToList();
            iterator++;

            gpsTravel.CityFrom = path[0];
            gpsTravel.CityTo = path[1];

            gpsTravels.Add(gpsTravel);
        }

        return gpsTravels;
    }
}