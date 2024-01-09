using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SecurityCore.UseCases;

public class UcForgotPassword(IMediator mediator) : UseCase, IUcForgotPassword
{
    public async Task<ISingleResult<Entity>> Execute(ForgotPasswordCommand entity)
    {
        return await mediator.Send(entity);
    }
}
