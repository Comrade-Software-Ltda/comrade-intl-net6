using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemRoleCore;

public interface IUcSystemRoleEdit
{
    Task<ISingleResult<Entity>> Execute(SystemRoleEditCommand entity);
}
