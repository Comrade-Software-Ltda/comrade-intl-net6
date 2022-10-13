using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore.Validations;

public interface ISystemPermissionTagUniqueValidation
{
    Task<ISingleResult<Entity>> Execute(SystemPermission entity);
}
