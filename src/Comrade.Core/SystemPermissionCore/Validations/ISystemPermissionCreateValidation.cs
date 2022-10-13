using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore.Validations;

public interface ISystemPermissionCreateValidation
{
    Task<ISingleResult<Entity>> Execute(SystemPermission entity);
}
