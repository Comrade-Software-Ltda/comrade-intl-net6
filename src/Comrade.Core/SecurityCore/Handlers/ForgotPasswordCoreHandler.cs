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
    ForgotPasswordCoreHandler : IRequestHandler<ForgotPasswordCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISystemUserRepository _repository;
    private readonly ISystemUserForgotPasswordValidation _systemUserForgotPasswordValidation;

    public ForgotPasswordCoreHandler(IPasswordHasher passwordHasher,
        ISystemUserRepository repository,
        ISystemUserForgotPasswordValidation systemUserForgotPasswordValidation,
        IMongoDbCommandContext mongoDbContext)
    {
        _passwordHasher = passwordHasher;
        _repository = repository;
        _systemUserForgotPasswordValidation = systemUserForgotPasswordValidation;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var result = _systemUserForgotPasswordValidation.Execute(request, recordExists);
        if (!result.Success) return result;

        var obj = recordExists;

        HydrateValues(obj);

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Update(obj);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        _mongoDbContext.ReplaceOne(obj);

        return new EditResult<Entity>();
    }

    private void HydrateValues(SystemUser target)
    {
        var ruleForgotPassword = "123456";
        target.Password = _passwordHasher.Hash(ruleForgotPassword);
    }
}
