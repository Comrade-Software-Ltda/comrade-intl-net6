using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemPermissionCore.UseCases;

public class UcSystemPermissionDelete(IMediator mediator) : UseCase, IUcSystemPermissionDelete
{
    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new SystemPermissionDeleteCommand {Id = id};
        return await mediator.Send(entity);
    }
}
