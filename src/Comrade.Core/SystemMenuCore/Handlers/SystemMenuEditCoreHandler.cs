using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Core.SystemMenuCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemMenuCore.Handlers;

public class
    SystemMenuEditCoreHandler(
        ISystemMenuEditValidation systemMenuEditValidation,
        ISystemMenuRepository repository,
        IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<SystemMenuEditCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemMenuEditCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await repository.GetById(request.Id);

        if (recordExists is null)
        {
            return new EditResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = await systemMenuEditValidation.Execute(request, recordExists)
            ;

        if (!validate.Success)
        {
            return validate;
        }

        var obj = recordExists;
        HydrateValues(obj, request);

        await repository.BeginTransactionAsync();
        repository.Update(obj);
        await repository.CommitTransactionAsync();

        mongoDbContext.ReplaceOne(obj);

        return new EditResult<Entity>(true,
            BusinessMessage.MSG02);
    }

    private static void HydrateValues(SystemMenu target, SystemMenu source)
    {
        target.Description = source.Description;
        target.Menu = source.Menu;
        target.Route = source.Route;
        target.Title = source.Title;
        target.Icon = source.Icon?.Trim().ToLower(CultureInfo.CurrentCulture);
    }
}
