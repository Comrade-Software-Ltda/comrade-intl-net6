using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleTagUniqueValidation(ISystemRoleRepository repository) : ISystemRoleTagUniqueValidation
{
    public async Task<ISingleResult<Entity>> Execute(SystemRole entity)
    {
        var result = await repository.UniqueTagValidation(entity.Tag);
        return result.Success
            ? new SingleResult<Entity>(entity)
            : new SingleResult<Entity>(result.Code, result.Message);
    }
}
