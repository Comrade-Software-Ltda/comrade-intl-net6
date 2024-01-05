using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SecurityCore.Handlers;

public class
    UpdatePasswordCoreHandler(
        IPasswordHasher passwordHasher,
        ISystemUserRepository repository,
        ISystemUserEditValidation systemUserEditValidation,
        IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<UpdatePasswordCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(UpdatePasswordCommand request,
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

        await repository.BeginTransactionAsync();
        repository.Update(obj);
        await repository.CommitTransactionAsync();

        mongoDbContext.ReplaceOne(obj);

        return new SingleResult<Entity>(request);
    }

    private void HydrateValues(SystemUser target, SystemUser source)
    {
        target.Password = passwordHasher.Hash(source.Password);
    }
}
