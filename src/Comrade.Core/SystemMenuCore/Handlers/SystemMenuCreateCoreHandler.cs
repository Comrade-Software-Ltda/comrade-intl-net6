using System.Threading;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Extensions;
using MediatR;
using Comrade.Core.SystemMenuCore.Validations;

namespace Comrade.Core.SystemMenuCore.Handlers;

public class
    SystemMenuCreateCoreHandler : IRequestHandler<SystemMenuCreateCommand, ISingleResult<Entity>>
{
    private readonly SystemMenuCreateValidation _systemMenuCreateValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemMenuRepository _repository;

    public SystemMenuCreateCoreHandler(SystemMenuCreateValidation systemMenuCreateValidation,
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

        _mongoDbContext.InsertOne(request);

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }
}