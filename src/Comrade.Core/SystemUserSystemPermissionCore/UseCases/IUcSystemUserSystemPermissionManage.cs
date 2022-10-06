using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserSystemPermissionCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemUserSystemPermissionCore.UseCases;

public interface IUcSystemUserSystemPermissionManage
{
    Task<ISingleResult<Entity>> Execute(SystemUserSystemPermissionManageCommand entity);
}