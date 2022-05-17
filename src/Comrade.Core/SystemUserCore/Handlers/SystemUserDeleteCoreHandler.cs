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
    SystemUserDeleteCoreHandler : IRequestHandler<SystemUserDeleteCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemUserRepository _repository;
    private readonly ISystemUserDeleteValidation _systemUserDeleteValidation;

    public SystemUserDeleteCoreHandler(ISystemUserDeleteValidation systemUserDeleteValidation,
        ISystemUserRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _systemUserDeleteValidation = systemUserDeleteValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemUserDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = _systemUserDeleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        var systemUserId = recordExists.Id;
        _repository.Remove(systemUserId);

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Remove(systemUserId);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        _mongoDbContext.DeleteOne<SystemUser>(systemUserId);

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG03);
    }
}