using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Core.SystemPermissionCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemPermissionCore.Handlers;

public class SystemPermissionDeleteCoreHandler(
    ISystemPermissionDeleteValidation deleteValidation,
    ISystemPermissionRepository repository,
    IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<SystemPermissionDeleteCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemPermissionDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await repository.GetById(request.Id);
        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false, BusinessMessage.MSG04);
        }

        var validate = deleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        var id = recordExists.Id;
        repository.Remove(id);
        await repository.BeginTransactionAsync();
        repository.Remove(id);
        await repository.CommitTransactionAsync();
        mongoDbContext.DeleteOne<SystemPermission>(id);
        return new DeleteResult<Entity>(true, BusinessMessage.MSG03);
    }
}
