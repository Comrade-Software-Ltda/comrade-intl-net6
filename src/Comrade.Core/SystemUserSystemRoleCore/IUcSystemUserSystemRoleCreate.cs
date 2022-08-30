using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemUserSystemRoleCore;

public interface IUcSystemUserSystemRoleCreate
{
    Task<ISingleResult<Entity>> Execute(SystemUserSystemRoleCreateCommand entity);
}