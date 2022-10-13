namespace Comrade.Domain.Bases.Interfaces;

public interface ILookup
{
    Guid Key { get; set; }
    string? Value { get; set; }
}
