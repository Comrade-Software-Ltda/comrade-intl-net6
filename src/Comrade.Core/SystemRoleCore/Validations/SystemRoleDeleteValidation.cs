using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleDeleteValidation : ISystemRoleDeleteValidation
{
    public ISingleResult<Entity> Execute(SystemRole? recordExists)
    {
        return new SingleResult<Entity>(recordExists);
    }
}
