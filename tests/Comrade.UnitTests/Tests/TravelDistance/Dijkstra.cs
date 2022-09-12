using System.Linq;

namespace Comrade.UnitTests.Tests.TravelDistance;

public class Dijkstra : IShortestPathFinder
{
    private readonly List<string> _cityHeavyTraffics = new() {"a", "e", "i", "o", "u"};


    public int FindShortestPath(GpsTravel gpsTravel)
    {
        var nodes = PrepareNodes(gpsTravel);


        var from = nodes.Where(x => x.Label.Equals(gpsTravel.CityFrom)).First();
        var to = nodes.Where(x => x.Label.Equals(gpsTravel.CityTo)).First();


        var control = new VisitingData();

        control.UpdateWeight(from, new Weight(null, 0));
        control.ScheduleVisitTo(from);

        while (control.HasScheduledVisits)
        {
            var visitingNode = control.GetNodeToVisit();
            var visitingNodeWeight = control.QueryWeight(visitingNode);
            control.RegisterVisitTo(visitingNode);

            foreach (var neighborhoodInfo in visitingNode.Neighbors)
            {
                if (!control.WasVisited(neighborhoodInfo.Node))
                {
                    control.ScheduleVisitTo(neighborhoodInfo.Node);
                }

                var neighborWeight = control.QueryWeight(neighborhoodInfo.Node);

                var probableWeight = visitingNodeWeight.Value + neighborhoodInfo.WeightToNode;
                if (neighborWeight.Value > probableWeight)
                {
                    control.UpdateWeight(neighborhoodInfo.Node, new Weight(visitingNode, probableWeight));
                }
            }
        }

        var shortestPath = control.ComputedPathToOrigin(to).Reverse().ToArray();

        var timeToTravel = TimeToTravel(gpsTravel, shortestPath);

        return timeToTravel;
    }

    private List<Node> PrepareNodes(GpsTravel gpsTravel)
    {
        var nodes = new List<Node>();
        foreach (var node in gpsTravel.CityNames)
        {
            nodes.Add(new Node(node));
        }

        foreach (var route in gpsTravel.DistanceCities)
        {
            var city1 = nodes.Where(x => x.Label.Equals(route.City1)).First();
            var city2 = nodes.Where(x => x.Label.Equals(route.City2)).First();

            if (_cityHeavyTraffics.Contains(city1.Label))
            {
                city1.ConnectTo(city2, route.Distance + 5);
            }
            else
            {
                city1.ConnectTo(city2, route.Distance);
            }
        }

        return nodes;
    }

    private int TimeToTravel(GpsTravel gpsTravel, Node[] shortestPath)
    {
        var timeToTravel = 0;

        for (var i = 0; i < shortestPath!.Length - 1; i++)
        {
            if (shortestPath != null)
            {
                if (_cityHeavyTraffics.Contains(shortestPath[i].Label))
                {
                    timeToTravel += gpsTravel.DistanceCities
                        .Where(x => x.City1.Equals(shortestPath[i].Label) && x.City2.Equals(shortestPath[i + 1].Label))
                        .Select(y => y.Distance + 5).First();
                }
                else
                {
                    timeToTravel += gpsTravel.DistanceCities
                        .Where(x => x.City1.Equals(shortestPath[i].Label) && x.City2.Equals(shortestPath[i + 1].Label))
                        .Select(y => y.Distance).First();
                }
            }
        }

        return timeToTravel;
    }
}