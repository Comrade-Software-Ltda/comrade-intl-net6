namespace Comrade.Domain.Models;

public class Delivery
{
    public Drone AssignedDrone { get; set; }
    public List<Location> Locations { get; set; }
    public double TotalWeight => Locations.Sum(loc => loc.PackageWeight);
}
