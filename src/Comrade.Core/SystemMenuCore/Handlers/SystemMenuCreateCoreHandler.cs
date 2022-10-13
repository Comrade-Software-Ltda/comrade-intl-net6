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
    SystemMenuCreateCoreHandler : IRequestHandler<SystemMenuCreateCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemMenuRepository _repository;
    private readonly ISystemMenuCreateValidation _systemMenuCreateValidation;

    public SystemMenuCreateCoreHandler(ISystemMenuCreateValidation systemMenuCreateValidation,
        ISystemMenuRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _systemMenuCreateValidation = systemMenuCreateValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemMenuCreateCommand request,
        CancellationToken cancellationToken)
    {
        var validate = await _systemMenuCreateValidation.Execute(request).ConfigureAwait(false);
        if (!validate.Success)
        {
            return validate;
        }

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        await _repository.Add(request).ConfigureAwait(false);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        SaveInMongo(request);

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }

    private void SaveInMongo(SystemMenuCreateCommand request)
    {
        if (request.Menu != null) request.Menu.Submenus = new List<SystemMenu>();
        if (request.Submenus != null && request.Submenus.Any()) request.Submenus.ForEach(x => { x.Menu = null; });
        _mongoDbContext.InsertOne(request);
    }
}
