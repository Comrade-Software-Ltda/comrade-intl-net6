using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.UseCases;

public class UcAirplaneDelete : UseCase, IUcAirplaneDelete
{
    private readonly AirplaneDeleteValidation _airplaneDeleteValidation;
    private readonly IAirplaneRepository _repository;

    public UcAirplaneDelete(IAirplaneRepository repository,
        AirplaneDeleteValidation airplaneDeleteValidation,
        IUnitOfWork uow)
        : base(uow)
    {
        _repository = repository;
        _airplaneDeleteValidation = airplaneDeleteValidation;
    }

    public async Task<ISingleResult<Airplane>> Execute(int id)
    {
        var validate = await _airplaneDeleteValidation.Execute(id).ConfigureAwait(false);
        if (!validate.Success) return validate;

        _repository.Remove(id);

        _ = await Commit().ConfigureAwait(false);

        return new DeleteResult<Airplane>(true,
            BusinessMessage.ResourceManager.GetString("MSG03", CultureInfo.CurrentCulture));
    }
}