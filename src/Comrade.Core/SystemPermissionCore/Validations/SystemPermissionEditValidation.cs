using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore.Validations;

public class SystemPermissionEditValidation : ISystemPermissionEditValidation
{
    private readonly ISystemPermissionTagUniqueValidation _systemPermissionTagUniqueValidation;

    public SystemPermissionEditValidation(ISystemPermissionTagUniqueValidation systemPermissionTagUniqueValidation)
    {
        _systemPermissionTagUniqueValidation = systemPermissionTagUniqueValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemPermission entity, SystemPermission? recordExists)
    {
        var register = await _systemPermissionTagUniqueValidation.Execute(entity).ConfigureAwait(false);
        return register.Success ? new SingleResult<Entity>(recordExists) : register;
    }
}
