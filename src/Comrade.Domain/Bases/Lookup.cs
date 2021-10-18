using Comrade.Domain.Bases.Interfaces;

namespace Comrade.Domain.Bases;

public class Lookup : ILookup
{
    public int Key { get; set; }
    public string? Value { get; set; }
}