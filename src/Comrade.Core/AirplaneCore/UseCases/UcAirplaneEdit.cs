using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.UseCases;

public class UcAirplaneEdit : UseCase, IUcAirplaneEdit
{
    private readonly AirplaneEditValidation _airplaneEditValidation;
    private readonly IAirplaneRepository _repository;

    public UcAirplaneEdit(IAirplaneRepository repository,
        AirplaneEditValidation airplaneEditValidation,
        IUnitOfWork uow)
        : base(uow)
    {
        _repository = repository;
        _airplaneEditValidation = airplaneEditValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(AirplaneEditCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        var recordExists = await _repository.GetById(entity.Id).ConfigureAwait(false);

        var result = await _airplaneEditValidation.Execute(entity, recordExists)
            .ConfigureAwait(false);

        if (!result.Success)
        {
            return result;
        }

        var obj = recordExists;

        HydrateValues(obj, entity);

        _repository.Update(obj);

        _ = await Commit().ConfigureAwait(false);

        return new EditResult<Entity>(true,
            BusinessMessage.MSG02);
    }

    private static void HydrateValues(Airplane target, Airplane source)
    {
        target.Code = source.Code;
        target.PassengerQuantity = source.PassengerQuantity;
        target.Model = source.Model;
    }
}