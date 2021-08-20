#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Bases.Validations;
using Comrade.Core.SystemUserCore;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SecurityCore.Validation;

public class SystemUserPasswordValidation : EntityValidation<SystemUser>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISystemUserRepository _systemUserRepository;

    public SystemUserPasswordValidation(ISystemUserRepository systemUserRepository,
        IPasswordHasher passwordHasher)
        : base(systemUserRepository)
    {
        _systemUserRepository = systemUserRepository;
        _passwordHasher = passwordHasher;
    }

    public ISingleResult<SystemUser> Execute(int key, string password)
    {
        var usuSession = _systemUserRepository.GetById(key).Result;
        var keyValidation = usuSession != null;

        if (keyValidation)
        {
            var passwordValidation = _passwordHasher.Check(usuSession!.Password, password);

            if (!passwordValidation.Verified)
            {
                return new SingleResult<SystemUser>(1001,
                    "Usuário ou password informados não são válidos");
            }


            return new SingleResult<SystemUser>(usuSession);
        }


        return new SingleResult<SystemUser>(1001,
            "Usuário ou password informados não são válidos");
    }
}
