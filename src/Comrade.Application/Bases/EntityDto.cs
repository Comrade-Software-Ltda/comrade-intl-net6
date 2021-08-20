#region

using Comrade.Application.Bases.Interfaces;

#endregion

namespace Comrade.Application.Bases;

public class EntityDto : Dto, IEntityDto
{
    public int Id { get; set; }
}
