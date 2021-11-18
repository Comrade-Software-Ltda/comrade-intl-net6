using System.Threading;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;
using Comrade.Core.SystemMenuCore.Validations;

namespace Comrade.Core.SystemMenuCore.Handlers;

public class
    SystemMenuDeleteCoreHandler : IRequestHandler<SystemMenuDeleteCommand, ISingleResult<Entity>>
{
    private readonly SystemMenuDeleteValidation _systemMenuDeleteValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemMenuRepository _repository;

    public SystemMenuDeleteCoreHandler(SystemMenuDeleteValidation systemMenuDeleteValidation,
        ISystemMenuRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _systemMenuDeleteValidation = systemMenuDeleteValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemMenuDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = _systemMenuDeleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        var systemMenuId = recordExists.Id;
        _repository.Remove(systemMenuId);

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Remove(systemMenuId);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        _mongoDbContext.DeleteOne<SystemMenu>(systemMenuId);

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG03);
    }
}