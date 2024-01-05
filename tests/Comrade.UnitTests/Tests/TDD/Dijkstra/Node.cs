using System.Linq;

namespace Comrade.UnitTests.Tests.TDD.Dijkstra;

public class Node(string label)
{
    public string Label { get; } = label;
    private List<Edge> Edges { get; } = new();

    public IEnumerable<NeighborhoodInfo> Neighbors =>
        from edge in Edges
        select new NeighborhoodInfo(
            edge.Node1 == this ? edge.Node2 : edge.Node1,
            edge.Value
        );

    private void Assign(Edge edge)
    {
        Edges.Add(edge);
    }

    public void ConnectTo(Node other, int connectionValue)
    {
        Edge.Create(connectionValue, this, other);
    }

    public struct NeighborhoodInfo(Node node, int weightToNode)
    {
        public Node Node { get; } = node;
        public int WeightToNode { get; } = weightToNode;
    }

    public class Edge
    {
        public Edge(int value, Node node1, Node node2)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Edge value needs to be positive.");
            }

            Value = value;
            Node1 = node1;
            node1.Assign(this);
            Node2 = node2;
            node2.Assign(this);
        }

        public int Value { get; }
        public Node Node1 { get; }
        public Node Node2 { get; }

        public static Edge Create(int value, Node node1, Node node2)
        {
            return new Edge(value, node1, node2);
        }
    }
}
