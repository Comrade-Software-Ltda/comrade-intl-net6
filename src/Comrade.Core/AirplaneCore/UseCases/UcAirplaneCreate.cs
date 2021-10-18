using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.UseCases;

public class UcAirplaneCreate : UseCase, IUcAirplaneCreate
{
    private readonly AirplaneCreateValidation _airplaneCreateValidation;
    private readonly IAirplaneRepository _repository;

    public UcAirplaneCreate(IAirplaneRepository repository,
        AirplaneCreateValidation airplaneCreateValidation,
        IUnitOfWork uow)
        : base(uow)
    {
        _repository = repository;
        _airplaneCreateValidation = airplaneCreateValidation;
    }

    public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        var validate = await _airplaneCreateValidation.Execute(entity).ConfigureAwait(false);
        if (!validate.Success) return validate;
        entity.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();
        await _repository.Add(entity).ConfigureAwait(false);

        _ = await Commit().ConfigureAwait(false);

        return new CreateResult<Airplane>(true,
            BusinessMessage.ResourceManager.GetString("MSG01", CultureInfo.CurrentCulture));
    }
}