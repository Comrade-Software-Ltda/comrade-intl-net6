using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleCreateValidation : ISystemRoleCreateValidation
{
    private readonly ISystemRoleNameUniqueValidation _systemRoleNameUniqueValidation;

    public SystemRoleCreateValidation(ISystemRoleNameUniqueValidation systemRoleNameUniqueValidation)
    {
        _systemRoleNameUniqueValidation = systemRoleNameUniqueValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemRole entity)
    {
        var register = await _systemRoleNameUniqueValidation.Execute(entity).ConfigureAwait(false);
        return register.Success ? new SingleResult<Entity>(entity) : register;
    }
}
