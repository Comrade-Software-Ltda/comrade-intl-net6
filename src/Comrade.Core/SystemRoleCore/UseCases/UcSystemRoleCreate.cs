using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemRoleCore.UseCases;

public class UcSystemRoleCreate : UseCase, IUcSystemRoleCreate
{
    private readonly IMediator _mediator;

    public UcSystemRoleCreate(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemRoleCreateCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}
