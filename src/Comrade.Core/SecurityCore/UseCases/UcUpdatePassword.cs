using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SecurityCore.UseCases;

public class UcUpdatePassword : UseCase, IUcUpdatePassword
{
    private readonly IMediator _mediator;

    public UcUpdatePassword(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(UpdatePasswordCommand entity)
    {
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}