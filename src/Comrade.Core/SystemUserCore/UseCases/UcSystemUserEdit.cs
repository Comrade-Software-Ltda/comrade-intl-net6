using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserEdit : UseCase, IUcSystemUserEdit
{
    private readonly ISystemUserRepository _repository;
    private readonly SystemUserEditValidation _systemUserEditValidation;

    public UcSystemUserEdit(ISystemUserRepository repository,
        SystemUserEditValidation systemUserEditValidation, IUnitOfWork uow)
        : base(uow)
    {
        _repository = repository;
        _systemUserEditValidation = systemUserEditValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemUser entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

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

        _repository.Update(obj);

        _ = await Commit().ConfigureAwait(false);

        return new EditResult<Entity>();
    }

    private static void HydrateValues(SystemUser target, SystemUser source)
    {
        target.Name = source.Name;
        target.Email = source.Email;
        target.Registration = source.Registration;
    }
}