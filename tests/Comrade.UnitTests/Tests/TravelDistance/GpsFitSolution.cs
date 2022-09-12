using Xunit;
using Xunit.Abstractions;

namespace Comrade.UnitTests.Tests.TravelDistance;

public class GpsFitSolution
{

    private readonly Dijkstra _shortestPathFinder;

    public GpsFitSolution()
    {
        _shortestPathFinder = new Dijkstra();
    }

    public List<int> Result(string filePath)
    {
        var gpsTravels = DataTransformation.ExtractDataFromFile(filePath);

        var result = new List<int>();

        foreach (var item in gpsTravels)
        {
            var shortestPath = _shortestPathFinder.FindShortestPath(item);
            result.Add(shortestPath);
        }

        return result;
    }
}

