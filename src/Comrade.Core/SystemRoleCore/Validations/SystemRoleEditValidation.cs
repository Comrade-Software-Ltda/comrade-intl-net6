using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleEditValidation(ISystemRoleNameUniqueValidation systemRoleNameUniqueValidation)
    : ISystemRoleEditValidation
{
    public async Task<ISingleResult<Entity>> Execute(SystemRole entity, SystemRole? recordExists)
    {
        var register = await systemRoleNameUniqueValidation.Execute(entity);
        return register.Success ? new SingleResult<Entity>(recordExists) : register;
    }
}
