using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemRoleCore.UseCases;

public class UcSystemRoleDelete(IMediator mediator) : UseCase, IUcSystemRoleDelete
{
    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new SystemRoleDeleteCommand {Id = id};
        return await mediator.Send(entity);
    }
}
