using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public interface ISystemUserManagePermissionsValidation
{
    ISingleResult<Entity> Execute(SystemUser entity);
}
