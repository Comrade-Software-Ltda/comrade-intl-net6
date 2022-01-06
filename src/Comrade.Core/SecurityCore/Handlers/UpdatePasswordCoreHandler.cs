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
    UpdatePasswordCoreHandler : IRequestHandler<UpdatePasswordCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISystemUserRepository _repository;
    private readonly ISystemUserEditValidation _systemUserEditValidation;

    public UpdatePasswordCoreHandler(IPasswordHasher passwordHasher,
        ISystemUserRepository repository, ISystemUserEditValidation systemUserEditValidation,
        IMongoDbCommandContext mongoDbContext)
    {
        _passwordHasher = passwordHasher;
        _repository = repository;
        _systemUserEditValidation = systemUserEditValidation;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(UpdatePasswordCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var result = _systemUserEditValidation.Execute(request, recordExists);
        if (!result.Success)
        {
            return result;
        }

        var obj = recordExists;

        HydrateValues(obj, request);

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Update(obj);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        _mongoDbContext.ReplaceOne(obj);

        return new SingleResult<Entity>(request);
    }

    private void HydrateValues(SystemUser target, SystemUser source)
    {
        target.Password = _passwordHasher.Hash(source.Password);
    }
}