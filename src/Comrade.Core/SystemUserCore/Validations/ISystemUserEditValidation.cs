using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public interface ISystemUserEditValidation
{
    ISingleResult<Entity> Execute(SystemUser entity, SystemUser? recordExists);
}