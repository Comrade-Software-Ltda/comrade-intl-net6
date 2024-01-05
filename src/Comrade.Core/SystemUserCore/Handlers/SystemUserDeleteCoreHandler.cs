using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemUserCore.Handlers;

public class
    SystemUserDeleteCoreHandler(
        ISystemUserDeleteValidation systemUserDeleteValidation,
        ISystemUserRepository repository,
        IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<SystemUserDeleteCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemUserDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await repository.GetById(request.Id);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = systemUserDeleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        var systemUserId = recordExists.Id;
        repository.Remove(systemUserId);

        await repository.BeginTransactionAsync();
        repository.Remove(systemUserId);
        await repository.CommitTransactionAsync();

        mongoDbContext.DeleteOne<SystemUser>(systemUserId);

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG03);
    }
}
