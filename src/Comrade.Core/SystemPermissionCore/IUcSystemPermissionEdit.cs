using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemPermissionCore;

public interface IUcSystemPermissionEdit
{
    Task<ISingleResult<Entity>> Execute(SystemPermissionEditCommand entity);
}
