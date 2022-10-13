using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;

namespace Comrade.Application.Lookups;

public class LookupDto : EntityDto, ILookupDto
{
    public Guid Key { get; set; }
    public string Value { get; set; } = "";
}
