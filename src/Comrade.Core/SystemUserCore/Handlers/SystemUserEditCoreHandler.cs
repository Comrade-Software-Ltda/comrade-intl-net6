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
    SystemUserEditCoreHandler(
        ISystemUserEditValidation systemUserEditValidation,
        ISystemUserRepository repository,
        IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<SystemUserEditCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemUserEditCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await repository.GetById(request.Id);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var result = systemUserEditValidation.Execute(request, recordExists);
        if (!result.Success)
        {
            return result;
        }

        var obj = recordExists;

        HydrateValues(obj, request);

        repository.Update(obj);

        await repository.BeginTransactionAsync();
        repository.Update(obj);
        await repository.CommitTransactionAsync();

        mongoDbContext.ReplaceOne(obj);

        return new EditResult<Entity>(true,
            BusinessMessage.MSG02);
    }

    private static void HydrateValues(SystemUser target, SystemUser source)
    {
        target.Name = source.Name;
        target.Email = source.Email;
        target.Registration = source.Registration;
    }
}
