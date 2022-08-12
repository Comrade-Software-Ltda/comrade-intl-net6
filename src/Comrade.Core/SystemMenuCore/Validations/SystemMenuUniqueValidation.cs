using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemMenuCore.Validations;

public class SystemMenuUniqueValidation : ISystemMenuUniqueValidation
{
    private readonly ISystemMenuRepository _repository;

    public SystemMenuUniqueValidation(ISystemMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemMenu entity)
    {
        var registerSameCode =
            await _repository.CodeUniqueValidation(entity.MenuId, entity.Text!)
                .ConfigureAwait(false);

        if (registerSameCode.Success)
        {
            return new SingleResult<Entity>(entity);
        }

        return registerSameCode;
    }
}