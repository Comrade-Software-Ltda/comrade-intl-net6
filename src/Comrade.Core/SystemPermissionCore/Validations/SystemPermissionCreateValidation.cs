using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore.Validations;

public class SystemPermissionCreateValidation : ISystemPermissionCreateValidation
{
    private readonly ISystemPermissionTagUniqueValidation _systemPermissionTagUniqueValidation;

    public SystemPermissionCreateValidation(ISystemPermissionTagUniqueValidation systemPermissionTagUniqueValidation)
    {
        _systemPermissionTagUniqueValidation = systemPermissionTagUniqueValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemPermission entity)
    {
        var register = await _systemPermissionTagUniqueValidation.Execute(entity).ConfigureAwait(false);
        return register.Success ? new SingleResult<Entity>(entity) : register;
    }
}
