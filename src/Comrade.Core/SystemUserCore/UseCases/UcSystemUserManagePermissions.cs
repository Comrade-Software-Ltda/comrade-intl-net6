using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserManagePermissions(IMediator mediator) : UseCase, IUcSystemUserManagePermissions
{
    public async Task<ISingleResult<Entity>> Execute(SystemUserManagePermissionsCommand entity)
    {
        return await mediator.Send(entity);
    }
}
