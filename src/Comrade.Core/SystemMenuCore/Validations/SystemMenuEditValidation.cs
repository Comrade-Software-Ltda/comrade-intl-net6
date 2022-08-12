using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemMenuCore.Validations;

public class SystemMenuEditValidation : ISystemMenuEditValidation
{
    private readonly ISystemMenuUniqueValidation _systemMenuValidateSameCode;

    public SystemMenuEditValidation(ISystemMenuRepository repository,
        ISystemMenuUniqueValidation systemMenuValidateSameCode)
    {
        _systemMenuValidateSameCode = systemMenuValidateSameCode;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemMenu entity, SystemMenu? recordExists)
    {
        var registerSameCode =
            await _systemMenuValidateSameCode.Execute(entity).ConfigureAwait(false);

        if (registerSameCode.Success)
        {
            return new SingleResult<Entity>(entity);
        }

        return registerSameCode;
    }
}