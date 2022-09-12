using System.Linq;

namespace Comrade.UnitTests.Tests.TravelDistance;

internal class VisitingData
{
    private readonly List<Node> _scheduled = new();
    private readonly List<Node> _visits = new();

    private readonly Dictionary<Node, Weight> _weights = new();

    public bool HasScheduledVisits => _scheduled.Count > 0;

    public void RegisterVisitTo(Node node)
    {
        if (!_visits.Contains(node))
            _visits.Add(node);
    }

    public bool WasVisited(Node node)
    {
        return _visits.Contains(node);
    }

    public void UpdateWeight(Node node, Weight newWeight)
    {
        if (!_weights.ContainsKey(node))
        {
            _weights.Add(node, newWeight);
        }
        else
        {
            _weights[node] = newWeight;
        }
    }

    public Weight QueryWeight(Node node)
    {
        Weight result;
        if (!_weights.ContainsKey(node))
        {
            result = new Weight(null, int.MaxValue);
            _weights.Add(node, result);
        }
        else
        {
            result = _weights[node];
        }

        return result;
    }

    public void ScheduleVisitTo(Node node)
    {
        _scheduled.Add(node);
    }

    public Node GetNodeToVisit()
    {
        var ordered = from n in _scheduled
            orderby QueryWeight(n).Value
            select n;

        var result = ordered.First();
        _scheduled.Remove(result);
        return result;
    }

    public bool HasComputedPathToOrigin(Node node)
    {
        return QueryWeight(node).From != null;
    }

    public IEnumerable<Node> ComputedPathToOrigin(Node node)
    {
        var n = node;
        while (n != null)
        {
            yield return n;
            n = QueryWeight(n).From;
        }
    }
}