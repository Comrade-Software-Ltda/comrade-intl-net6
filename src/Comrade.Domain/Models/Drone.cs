namespace Comrade.Domain.Models;

public class Drone : IComparable<Drone>
{
    public string Name { get; set; }
    public int MaxWeight { get; set; }

    public int CompareTo(Drone other)
    {
        if (other == null) return 1;

        return MaxWeight.CompareTo(other.MaxWeight);
    }
}
