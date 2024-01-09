using System.IO;
using Comrade.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Comrade.Application.Components.DeliveryOptimizer.Core;

public static class ProcessFile
{
    public static (List<Drone> Drones, List<Location> Locations) Execute(IFormFile file)
    {
        var drones = new List<Drone>();
        var locations = new List<Location>();

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.ReadLine() is { } line)
            {
                var values = line.Split(new[] {"], ["}, StringSplitOptions.None)
                    .Select(val => val.Trim('[', ' ', ']'))
                    .ToList();

                // Check for invalid format
                if (values.Count % 2 != 0)
                {
                    return (new List<Drone>(), new List<Location>());
                }

                for (var i = 0; i < values.Count; i += 2)
                {
                    if (i + 1 >= values.Count)
                        continue;

                    if (int.TryParse(values[i + 1], out var weightValue))
                    {
                        if (line.Contains("Drone"))
                        {
                            drones.Add(new Drone
                            {
                                Name = values[i],
                                MaxWeight = weightValue
                            });
                        }
                        else
                        {
                            locations.Add(new Location
                            {
                                Address = values[i],
                                PackageWeight = weightValue
                            });
                        }
                    }
                    else
                    {
                        return (new List<Drone>(), new List<Location>());
                    }
                }
            }
        }

        return (drones, locations);
    }
}
