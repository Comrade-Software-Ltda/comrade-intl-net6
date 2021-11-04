using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
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
        IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _systemUserEditValidation = systemUserEditValidation;
        _passwordHasher = passwordHasher;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemUser entity)
    {
        var recordExists = await _repository.GetById(entity.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var result = _systemUserEditValidation.Execute(entity, recordExists);
        if (!result.Success)
        {
            return result;
        }

        var obj = recordExists;

        HydrateValues(obj, entity);

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Update(obj);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);


        return new SingleResult<Entity>(entity);
    }

    private void HydrateValues(SystemUser target, SystemUser source)
    {
        target.Password = _passwordHasher.Hash(source.Password);
    }
}