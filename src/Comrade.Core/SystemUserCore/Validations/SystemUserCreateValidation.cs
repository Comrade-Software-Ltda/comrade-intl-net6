using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserCreateValidation : ISystemUserCreateValidation
{
    public ISingleResult<Entity> Execute(SystemUser entity)
    {
        return new SingleResult<Entity>(entity);
    }
}