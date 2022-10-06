using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemUserSystemRoleCore.UseCases;

public interface IUcSystemUserSystemRoleManage
{
    Task<ISingleResult<Entity>> Execute(SystemUserSystemRoleManageCommand entity);
}