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

public class SystemPermissionEditCoreHandler(
    ISystemPermissionEditValidation editValidation,
    ISystemPermissionRepository repository,
    IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<SystemPermissionEditCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemPermissionEditCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await repository.GetById(request.Id);
        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false, BusinessMessage.MSG04);
        }

        var result = await editValidation.Execute(request, recordExists);
        if (!result.Success)
        {
            return result;
        }

        var obj = recordExists;
        HydrateValues(obj, request);
        repository.Update(obj);
        await repository.BeginTransactionAsync();
        repository.Update(obj);
        await repository.CommitTransactionAsync();
        mongoDbContext.ReplaceOne(obj);
        return new EditResult<Entity>(true, BusinessMessage.MSG02);
    }

    private static void HydrateValues(SystemPermission target, SystemPermission source)
    {
        target.Name = source.Name.ToUpper(CultureInfo.CurrentCulture).Trim();
        target.Tag = source.Tag.ToUpper(CultureInfo.CurrentCulture).Trim();
    }
}
