namespace Comrade.Domain.Bases.Interfaces;

public interface ILookup
{
    int Key { get; set; }
    string? Value { get; set; }
}