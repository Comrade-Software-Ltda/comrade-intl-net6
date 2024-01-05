using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Core.SystemRoleCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemRoleCore.Handlers;

public class SystemRoleDeleteCoreHandler(
    ISystemRoleDeleteValidation deleteValidation,
    ISystemRoleRepository repository,
    IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<SystemRoleDeleteCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemRoleDeleteCommand request,
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

        mongoDbContext.DeleteOne<SystemRole>(id);

        return new DeleteResult<Entity>(true, BusinessMessage.MSG03);
    }
}
