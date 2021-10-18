using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

namespace Comrade.Core.SecurityCore.UseCases;

public class UcUpdatePassword : UseCase, IUcUpdatePassword
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISystemUserRepository _repository;
    private readonly SystemUserEditValidation _systemUserEditValidation;

    public UcUpdatePassword(ISystemUserRepository repository,
        SystemUserEditValidation systemUserEditValidation,
        IPasswordHasher passwordHasher, IUnitOfWork uow)
        : base(uow)
    {
        _repository = repository;
        _systemUserEditValidation = systemUserEditValidation;
        _passwordHasher = passwordHasher;
    }

    public async Task<ISingleResult<SystemUser>> Execute(SystemUser entity)
    {
        var result = await _systemUserEditValidation.Execute(entity).ConfigureAwait(false);
        if (!result.Success) return result;

        var obj = result.Data!;

        HydrateValues(obj, entity);

        _repository.Update(obj);

        _ = await Commit().ConfigureAwait(false);


        return new SingleResult<SystemUser>(entity);
    }

    private void HydrateValues(SystemUser target, SystemUser source)
    {
        target.Password = _passwordHasher.Hash(source.Password);
    }
}