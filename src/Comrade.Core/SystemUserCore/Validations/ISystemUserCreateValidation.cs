using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public interface ISystemUserCreateValidation
{
    ISingleResult<Entity> Execute(SystemUser entity);
}
