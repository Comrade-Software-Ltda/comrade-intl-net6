using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserCreate(IMediator mediator) : UseCase, IUcSystemUserCreate
{
    public async Task<ISingleResult<Entity>> Execute(SystemUserCreateCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        return await mediator.Send(entity);
    }
}
