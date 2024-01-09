using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.AirplaneCore.UseCases;

public class UcAirplaneCreate(IMediator mediator) : UseCase, IUcAirplaneCreate
{
    public async Task<ISingleResult<Entity>> Execute(AirplaneCreateCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        return await mediator.Send(entity);
    }
}
