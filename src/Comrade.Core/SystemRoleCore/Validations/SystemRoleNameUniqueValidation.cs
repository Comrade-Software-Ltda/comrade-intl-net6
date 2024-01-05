using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleNameUniqueValidation(ISystemRoleRepository repository) : ISystemRoleNameUniqueValidation
{
    public async Task<ISingleResult<Entity>> Execute(SystemRole entity)
    {
        var result = await repository.UniqueNameValidation(entity.Name);
        return result.Success
            ? new SingleResult<Entity>(entity)
            : new SingleResult<Entity>(result.Code, result.Message);
    }
}
