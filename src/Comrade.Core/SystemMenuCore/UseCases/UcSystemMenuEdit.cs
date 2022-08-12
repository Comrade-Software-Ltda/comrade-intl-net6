using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemMenuCore.UseCases;

public class UcSystemMenuEdit : UseCase, IUcSystemMenuEdit
{
    private readonly IMediator _mediator;

    public UcSystemMenuEdit(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemMenuEditCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}