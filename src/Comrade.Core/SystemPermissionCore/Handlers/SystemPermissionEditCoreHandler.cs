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

public class SystemPermissionEditCoreHandler : IRequestHandler<SystemPermissionEditCommand, ISingleResult<Entity>>
{
    private readonly ISystemPermissionEditValidation _editValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemPermissionRepository _repository;

    public SystemPermissionEditCoreHandler(ISystemPermissionEditValidation editValidation,
        ISystemPermissionRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _editValidation = editValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemPermissionEditCommand request,
        CancellationToken cancellationToken)
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

    private static void HydrateValues(SystemPermission target, SystemPermission source)
    {
        target.Name = source.Name.ToUpper(CultureInfo.CurrentCulture).Trim();
        target.Tag = source.Tag.ToUpper(CultureInfo.CurrentCulture).Trim();
    }
}
