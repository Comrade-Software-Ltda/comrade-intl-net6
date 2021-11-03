using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.AirplaneCore.UseCases;

public class UcAirplaneCreate : UseCase, IUcAirplaneCreate
{
    private readonly IMediator _mediator;

    public UcAirplaneCreate(IMediator mediator,
        IUnitOfWork uow)
        : base(uow)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(AirplaneCreateCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        try
        {
            return await _mediator.Send(entity).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}