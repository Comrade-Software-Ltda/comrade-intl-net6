using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

namespace Comrade.Core.SecurityCore.UseCases;

public class UcForgotPassword : UseCase, IUcForgotPassword
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISystemUserRepository _repository;
    private readonly SystemUserForgotPasswordValidation _systemUserForgotPasswordValidation;

    public UcForgotPassword(ISystemUserRepository repository,
        SystemUserForgotPasswordValidation systemUserForgotPasswordValidation,
        IPasswordHasher passwordHasher, IUnitOfWork uow)
        : base(uow)
    {
        _repository = repository;
        _systemUserForgotPasswordValidation = systemUserForgotPasswordValidation;
        _passwordHasher = passwordHasher;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemUser entity)
    {
        var recordExists = await _repository.GetById(entity.Id).ConfigureAwait(false);

        var result = _systemUserForgotPasswordValidation.Execute(entity, recordExists);
        if (!result.Success) return result;

        var obj = recordExists;

        HydrateValues(obj);

        _repository.Update(obj);

        _ = await Commit().ConfigureAwait(false);

        return new EditResult<Entity>();
    }

    private void HydrateValues(SystemUser target)
    {
        var ruleForgotPassword = "123456";
        target.Password = _passwordHasher.Hash(ruleForgotPassword);
    }
}