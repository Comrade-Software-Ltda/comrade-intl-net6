using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemPermissionCore;

public interface IUcSystemPermissionCreate
{
    Task<ISingleResult<Entity>> Execute(SystemPermissionCreateCommand entity);
}
