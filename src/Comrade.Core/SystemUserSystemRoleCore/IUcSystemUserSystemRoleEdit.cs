using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemUserSystemRoleCore;

public interface IUcSystemUserSystemRoleEdit
{
    Task<ISingleResult<Entity>> Execute(SystemUserSystemRoleEditCommand entity);
}