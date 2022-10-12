using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemRoleCore;

public interface IUcSystemRoleDelete
{
    Task<ISingleResult<Entity>> Execute(Guid id);
}
