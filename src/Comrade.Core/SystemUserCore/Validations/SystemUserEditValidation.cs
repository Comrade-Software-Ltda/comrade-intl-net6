using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserEditValidation : ISystemUserEditValidation
{
    public ISingleResult<Entity> Execute(SystemUser entity, SystemUser? recordExists)
    {
        return new SingleResult<Entity>(recordExists);
    }
}