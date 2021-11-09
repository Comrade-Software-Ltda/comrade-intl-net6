using Comrade.Application.Bases.Interfaces;

namespace Comrade.Application.Bases;

public class EntityDto : Dto, IEntityDto
{
    public Guid Id { get; set; }
}