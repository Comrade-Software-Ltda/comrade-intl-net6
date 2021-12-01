using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.SystemUserCore;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

namespace Comrade.Core.SecurityCore.Validation;

public class SystemUserPasswordValidation : ISystemUserPasswordValidation
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISystemUserRepository _systemUserRepository;

    public SystemUserPasswordValidation(ISystemUserRepository systemUserRepository,
        IPasswordHasher passwordHasher)
    {
        _systemUserRepository = systemUserRepository;
        _passwordHasher = passwordHasher;
    }

    public ISingleResult<SystemUser> Execute(Guid key, string password)
    {
        var usuSession = _systemUserRepository.GetById(key).Result;
        var keyValidation = usuSession != null;

        if (keyValidation)
        {
            var (verified, needsUpgrade) = _passwordHasher.Check(usuSession!.Password, password);

            if (!verified)
            {
                return new SingleResult<SystemUser>(1001,
                    "Usuário ou password informados não são válidos");
            }

            if (needsUpgrade)
            {
                return new SingleResult<SystemUser>(1009,
                    "Senha precisa ser atualizada");
            }


            return new SingleResult<SystemUser>(usuSession);
        }


        return new SingleResult<SystemUser>(1001,
            "Usuário ou password informados não são válidos");
    }
}