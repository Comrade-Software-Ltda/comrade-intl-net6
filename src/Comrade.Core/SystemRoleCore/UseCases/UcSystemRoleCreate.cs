using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemRoleCore.UseCases;

public class UcSystemRoleCreate(IMediator mediator) : UseCase, IUcSystemRoleCreate
{
    public async Task<ISingleResult<Entity>> Execute(SystemRoleCreateCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        return await mediator.Send(entity);
    }
}
