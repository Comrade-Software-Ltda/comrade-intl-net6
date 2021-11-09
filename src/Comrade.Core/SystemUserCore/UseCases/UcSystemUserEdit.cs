using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserEdit : UseCase, IUcSystemUserEdit
{
    private readonly IMediator _mediator;

    public UcSystemUserEdit(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemUserEditCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        return await _mediator.Send(entity).ConfigureAwait(false);
    }

    private static void HydrateValues(SystemUser target, SystemUser source)
    {
        target.Name = source.Name;
        target.Email = source.Email;
        target.Registration = source.Registration;
    }
}