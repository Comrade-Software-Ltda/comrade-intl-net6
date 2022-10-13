using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemPermissionCore.UseCases;

public class UcSystemPermissionDelete : UseCase, IUcSystemPermissionDelete
{
    private readonly IMediator _mediator;

    public UcSystemPermissionDelete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new SystemPermissionDeleteCommand {Id = id};
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}
