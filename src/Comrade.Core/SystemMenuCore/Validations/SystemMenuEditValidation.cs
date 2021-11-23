using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemMenuCore.Validations;

public class SystemMenuEditValidation : ISystemMenuEditValidation
{
    public Task<ISingleResult<Entity>> Execute(SystemMenu entity, SystemMenu? recordExists)
    {
        return null;
    }
}