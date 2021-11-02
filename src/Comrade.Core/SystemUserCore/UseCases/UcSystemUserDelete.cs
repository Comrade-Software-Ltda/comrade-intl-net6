using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserDelete : UseCase, IUcSystemUserDelete
{
    private readonly ISystemUserRepository _repository;
    private readonly SystemUserDeleteValidation _systemUserDeleteValidation;

    public UcSystemUserDelete(ISystemUserRepository repository,
        SystemUserDeleteValidation systemUserDeleteValidation,
        IUnitOfWork uow)
        : base(uow)
    {
        _repository = repository;
        _systemUserDeleteValidation = systemUserDeleteValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(int id)
    {
        var recordExists = await _repository.GetById(id).ConfigureAwait(false);

        var validate = _systemUserDeleteValidation.Execute(recordExists);
        if (!validate.Success) return validate;

        _repository.Remove(id);

        _ = await Commit().ConfigureAwait(false);

        return new DeleteResult<Entity>();
    }
}