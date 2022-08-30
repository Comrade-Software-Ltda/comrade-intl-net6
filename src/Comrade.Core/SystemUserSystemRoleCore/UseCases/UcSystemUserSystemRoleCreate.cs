using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserSystemRoleCore.UseCases;

public class UcSystemUserSystemRoleCreate : UseCase, IUcSystemUserSystemRoleCreate
{
    private readonly IMediator _mediator;

    public UcSystemUserSystemRoleCreate(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemUserSystemRoleCreateCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}