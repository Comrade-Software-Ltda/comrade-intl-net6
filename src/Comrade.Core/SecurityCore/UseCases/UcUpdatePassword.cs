using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SecurityCore.UseCases;

public class UcUpdatePassword(IMediator mediator) : UseCase, IUcUpdatePassword
{
    public async Task<ISingleResult<Entity>> Execute(UpdatePasswordCommand entity)
    {
        return await mediator.Send(entity);
    }
}
