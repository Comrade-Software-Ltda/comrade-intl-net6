using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemMenuCore.UseCases;

public class UcSystemMenuCreate(IMediator mediator) : UseCase, IUcSystemMenuCreate
{
    public async Task<ISingleResult<Entity>> Execute(SystemMenuCreateCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        return await mediator.Send(entity);
    }
}
