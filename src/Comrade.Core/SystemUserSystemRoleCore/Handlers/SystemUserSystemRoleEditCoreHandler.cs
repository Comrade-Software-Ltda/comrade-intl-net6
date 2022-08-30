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

public class SystemUserSystemRoleEditCoreHandler : IRequestHandler<SystemUserSystemRoleEditCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemUserSystemRoleRepository _repository;
    private readonly ISystemUserSystemRoleEditValidation _editValidation;

    public SystemUserSystemRoleEditCoreHandler(ISystemUserSystemRoleEditValidation editValidation, ISystemUserSystemRoleRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _editValidation = editValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemUserSystemRoleEditCommand request, CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);
        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false, BusinessMessage.MSG04);
        }
        var result = await _editValidation.Execute(request, recordExists).ConfigureAwait(false);
        if (!result.Success)
        {
            return result;
        }
        var obj = recordExists;
        HydrateValues(obj, request);
        _repository.Update(obj);
        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Update(obj);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);
        _mongoDbContext.ReplaceOne(obj);
        return new EditResult<Entity>(true, BusinessMessage.MSG02);
    }

    private static void HydrateValues(SystemUserSystemRole target, SystemUserSystemRole source)
    {
        target.SystemUserId = source.SystemUserId;
        target.SystemRoleId = source.SystemRoleId;
    }
}
