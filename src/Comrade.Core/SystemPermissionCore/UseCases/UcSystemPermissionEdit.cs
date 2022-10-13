using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemPermissionCore.UseCases;

public class UcSystemPermissionEdit : UseCase, IUcSystemPermissionEdit
{
    private readonly IMediator _mediator;

    public UcSystemPermissionEdit(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemPermissionEditCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}
