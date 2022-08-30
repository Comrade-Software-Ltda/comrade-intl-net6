using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemUserSystemRoleCore.Validations;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserSystemRoleCore.Handlers;

public class SystemUserSystemRoleCreateCoreHandler : IRequestHandler<SystemUserSystemRoleCreateCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemUserSystemRoleRepository _repository;
    private readonly ISystemUserSystemRoleCreateValidation _createValidation;

    public SystemUserSystemRoleCreateCoreHandler(ISystemUserSystemRoleCreateValidation createValidation, ISystemUserSystemRoleRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _createValidation = createValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemUserSystemRoleCreateCommand request, CancellationToken cancellationToken)
    {
        var result = await _createValidation.Execute(request).ConfigureAwait(false);
        if (!result.Success)
        {
            return result;
        }
        await _repository.Add(request).ConfigureAwait(false);
        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        await _repository.Add(request).ConfigureAwait(false);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);
        return new CreateResult<Entity>(true, BusinessMessage.MSG01);
    }
}