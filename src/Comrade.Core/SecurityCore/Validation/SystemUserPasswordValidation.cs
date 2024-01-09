using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.SystemUserCore;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

namespace Comrade.Core.SecurityCore.Validation;

public class SystemUserPasswordValidation(
    ISystemUserRepository systemUserRepository,
    IPasswordHasher passwordHasher)
    : ISystemUserPasswordValidation
{
    public ISingleResult<SystemUser> Execute(Guid key, string password)
    {
        var usuSession = systemUserRepository.GetById(key).Result;
        var keyValidation = usuSession != null;

        if (keyValidation)
        {
            var (verified, needsUpgrade) = passwordHasher.Check(usuSession!.Password, password);

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
