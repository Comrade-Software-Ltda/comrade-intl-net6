using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Core.SystemMenuCore.Validations;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemMenuCore.Handlers;

public class
    SystemMenuDeleteCoreHandler(
        SystemMenuDeleteValidation systemMenuDeleteValidation,
        ISystemMenuRepository repository)
    : IRequestHandler<SystemMenuDeleteCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemMenuDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await repository.GetById(request.Id);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = systemMenuDeleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        var systemMenuId = recordExists.Id;

        await repository.BeginTransactionAsync();
        repository.Remove(systemMenuId);
        await repository.CommitTransactionAsync();

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG03);
    }
}
