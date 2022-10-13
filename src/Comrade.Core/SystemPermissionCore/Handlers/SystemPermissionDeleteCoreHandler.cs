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

public class SystemPermissionDeleteCoreHandler : IRequestHandler<SystemPermissionDeleteCommand, ISingleResult<Entity>>
{
    private readonly ISystemPermissionDeleteValidation _deleteValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemPermissionRepository _repository;

    public SystemPermissionDeleteCoreHandler(ISystemPermissionDeleteValidation deleteValidation,
        ISystemPermissionRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _deleteValidation = deleteValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemPermissionDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);
        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false, BusinessMessage.MSG04);
        }

        var validate = _deleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        var id = recordExists.Id;
        _repository.Remove(id);
        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Remove(id);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);
        _mongoDbContext.DeleteOne<SystemPermission>(id);
        return new DeleteResult<Entity>(true, BusinessMessage.MSG03);
    }
}
