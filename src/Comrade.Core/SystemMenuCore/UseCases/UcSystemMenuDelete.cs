using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemMenuCore.UseCases;

public class UcSystemMenuDelete(IMediator mediator) : UseCase, IUcSystemMenuDelete
{
    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new SystemMenuDeleteCommand {Id = id};
        return await mediator.Send(entity);
    }
}
