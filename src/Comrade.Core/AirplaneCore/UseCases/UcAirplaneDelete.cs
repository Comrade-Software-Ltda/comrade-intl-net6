using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;

namespace Comrade.Core.AirplaneCore.UseCases;

public class UcAirplaneDelete : UseCase, IUcAirplaneDelete
{
    private readonly IAirplaneRepository _repository;
    private readonly AirplaneDeleteValidation _airplaneDeleteValidation;

    public UcAirplaneDelete(IAirplaneRepository repository,
        AirplaneDeleteValidation airplaneDeleteValidation, IUnitOfWork uow)
        : base(uow)
    {
        _repository = repository;
        _airplaneDeleteValidation = airplaneDeleteValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(int id)
    {
        var recordExists = await _repository.GetById(id).ConfigureAwait(false);

        var validate = _airplaneDeleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        _repository.Remove(recordExists.Id);

        _ = await Commit().ConfigureAwait(false);

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG03);
    }
}