using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemPermissionCore.UseCases;

public class UcSystemPermissionCreate(IMediator mediator) : UseCase, IUcSystemPermissionCreate
{
    public async Task<ISingleResult<Entity>> Execute(SystemPermissionCreateCommand entity)
    {
        var isValid = ValidateEntity(entity);
        if (!isValid.Success)
        {
            return isValid;
        }

        return await mediator.Send(entity);
    }
}
