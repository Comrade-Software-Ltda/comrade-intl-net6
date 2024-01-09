using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore.Validations;

public class SystemPermissionCreateValidation(ISystemPermissionTagUniqueValidation systemPermissionTagUniqueValidation)
    : ISystemPermissionCreateValidation
{
    public async Task<ISingleResult<Entity>> Execute(SystemPermission entity)
    {
        var register = await systemPermissionTagUniqueValidation.Execute(entity);
        return register.Success ? new SingleResult<Entity>(entity) : register;
    }
}
