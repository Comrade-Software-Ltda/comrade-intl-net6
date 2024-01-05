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
    ForgotPasswordCoreHandler(
        IPasswordHasher passwordHasher,
        ISystemUserRepository repository,
        ISystemUserForgotPasswordValidation systemUserForgotPasswordValidation,
        IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<ForgotPasswordCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await repository.GetById(request.Id);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var result = systemUserForgotPasswordValidation.Execute(request, recordExists);
        if (!result.Success) return result;

        var obj = recordExists;

        HydrateValues(obj);

        await repository.BeginTransactionAsync();
        repository.Update(obj);
        await repository.CommitTransactionAsync();

        mongoDbContext.ReplaceOne(obj);

        return new EditResult<Entity>();
    }

    private void HydrateValues(SystemUser target)
    {
        var ruleForgotPassword = "123456";
        target.Password = passwordHasher.Hash(ruleForgotPassword);
    }
}
