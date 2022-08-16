using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleEditValidation : ISystemRoleEditValidation
{
    public ISingleResult<Entity> Execute(SystemRole entity, SystemRole? recordExists)
    {
        return new SingleResult<Entity>(recordExists);
    }
}
