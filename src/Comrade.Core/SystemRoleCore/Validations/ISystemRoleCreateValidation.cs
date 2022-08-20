using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public interface ISystemRoleCreateValidation
{
    Task<ISingleResult<Entity>> Execute(SystemRole entity);
}
