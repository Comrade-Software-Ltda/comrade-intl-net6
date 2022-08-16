using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public interface ISystemRoleDeleteValidation
{
    ISingleResult<Entity> Execute(SystemRole? recordExists);
}
