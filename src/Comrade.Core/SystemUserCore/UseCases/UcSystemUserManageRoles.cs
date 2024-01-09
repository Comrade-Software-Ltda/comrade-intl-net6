using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserManageRoles(IMediator mediator) : UseCase, IUcSystemUserManageRoles
{
    public async Task<ISingleResult<Entity>> Execute(SystemUserManageRolesCommand entity)
    {
        return await mediator.Send(entity);
    }
}
