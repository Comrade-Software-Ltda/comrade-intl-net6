using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemUserSystemRoleCore.Validations;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemUserSystemRoleCore.Handlers;

public class SystemUserSystemRoleDeleteCoreHandler : IRequestHandler<SystemUserSystemRoleDeleteCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemUserSystemRoleRepository _repository;
    private readonly ISystemUserSystemRoleDeleteValidation _deleteValidation;

    public SystemUserSystemRoleDeleteCoreHandler(ISystemUserSystemRoleDeleteValidation deleteValidation, ISystemUserSystemRoleRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _deleteValidation = deleteValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemUserSystemRoleDeleteCommand request, CancellationToken cancellationToken)
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
        _mongoDbContext.DeleteOne<SystemUserSystemRole>(id);
        return new DeleteResult<Entity>(true, BusinessMessage.MSG03);
    }
}