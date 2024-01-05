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
    SystemMenuCreateCoreHandler(
        ISystemMenuCreateValidation systemMenuCreateValidation,
        ISystemMenuRepository repository,
        IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<SystemMenuCreateCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemMenuCreateCommand request,
        CancellationToken cancellationToken)
    {
        var validate = await systemMenuCreateValidation.Execute(request);
        if (!validate.Success)
        {
            return validate;
        }

        await repository.BeginTransactionAsync();
        await repository.Add(request);
        await repository.CommitTransactionAsync();

        SaveInMongo(request);

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }

    private void SaveInMongo(SystemMenuCreateCommand request)
    {
        if (request.Menu != null) request.Menu.Submenus = new List<SystemMenu>();
        if (request.Submenus != null && request.Submenus.Any()) request.Submenus.ForEach(x => { x.Menu = null; });
        mongoDbContext.InsertOne(request);
    }
}
