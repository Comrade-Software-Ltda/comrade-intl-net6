using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemUserSystemRoleCore;

public interface IUcSystemUserSystemRoleDelete
{
    Task<ISingleResult<Entity>> Execute(Guid id);
}