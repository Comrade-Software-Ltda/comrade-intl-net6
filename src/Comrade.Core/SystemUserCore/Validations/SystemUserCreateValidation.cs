using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserCreateValidation : EntityValidation<SystemUser>
{
    public SystemUserCreateValidation(ISystemUserRepository repository)
        : base(repository)
    {
    }

    public static ISingleResult<SystemUser> Execute(SystemUser entity)
    {
        return new SingleResult<SystemUser>(entity);
    }
}