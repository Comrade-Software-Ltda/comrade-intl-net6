using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemMenuCore.Validations;

public class SystemMenuUniqueValidation(ISystemMenuRepository repository) : ISystemMenuUniqueValidation
{
    public async Task<ISingleResult<Entity>> Execute(SystemMenu entity)
    {
        var registerSameCode =
                await repository.UniqueValidation(entity)
            ;

        return !registerSameCode.Success ? registerSameCode : new SingleResult<Entity>(entity);
    }
}
