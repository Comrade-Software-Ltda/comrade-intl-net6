using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore.Validations;

public class SystemPermissionEditValidation(ISystemPermissionTagUniqueValidation systemPermissionTagUniqueValidation)
    : ISystemPermissionEditValidation
{
    public async Task<ISingleResult<Entity>> Execute(SystemPermission entity, SystemPermission? recordExists)
    {
        var register = await systemPermissionTagUniqueValidation.Execute(entity);
        return register.Success ? new SingleResult<Entity>(recordExists) : register;
    }
}
