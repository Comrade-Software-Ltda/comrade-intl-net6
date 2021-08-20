#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserCreateValidation : EntityValidation<SystemUser>
{
    private readonly ISystemUserRepository _repository;

    public SystemUserCreateValidation(ISystemUserRepository repository)
        : base(repository)
    {
        _repository = repository;
    }

    public static ISingleResult<SystemUser> Execute(SystemUser entity)
    {
        return new SingleResult<SystemUser>(entity);
    }
}
