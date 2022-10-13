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

public class SystemRoleDeleteCoreHandler : IRequestHandler<SystemRoleDeleteCommand, ISingleResult<Entity>>
{
    private readonly ISystemRoleDeleteValidation _deleteValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemRoleRepository _repository;

    public SystemRoleDeleteCoreHandler(ISystemRoleDeleteValidation deleteValidation, ISystemRoleRepository repository,
        IMongoDbCommandContext mongoDbContext)
    {
        _deleteValidation = deleteValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemRoleDeleteCommand request,
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

        _mongoDbContext.DeleteOne<SystemRole>(id);

        return new DeleteResult<Entity>(true, BusinessMessage.MSG03);
    }
}
