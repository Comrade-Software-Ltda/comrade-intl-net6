using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemRoleCore.UseCases;

public class UcSystemRoleEdit(IMediator mediator) : UseCase, IUcSystemRoleEdit
{
    public async Task<ISingleResult<Entity>> Execute(SystemRoleEditCommand entity)
    {
        return await mediator.Send(entity);
    }
}
