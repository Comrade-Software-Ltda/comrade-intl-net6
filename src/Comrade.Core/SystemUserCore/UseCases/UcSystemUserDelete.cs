using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserDelete(IMediator mediator) : UseCase, IUcSystemUserDelete
{
    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new SystemUserDeleteCommand {Id = id};
        return await mediator.Send(entity);
    }
}
