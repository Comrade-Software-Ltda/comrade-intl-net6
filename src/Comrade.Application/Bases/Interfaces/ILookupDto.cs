namespace Comrade.Application.Bases.Interfaces;

public interface ILookupDto
{
    Guid Key { get; set; }
    string Value { get; set; }
}
