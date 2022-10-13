using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemUserCore;

public interface IUcSystemUserDelete
{
    Task<ISingleResult<Entity>> Execute(Guid id);
}
