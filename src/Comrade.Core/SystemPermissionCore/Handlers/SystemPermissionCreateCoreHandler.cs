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

public class SystemPermissionCreateCoreHandler : IRequestHandler<SystemPermissionCreateCommand, ISingleResult<Entity>>
{
    private readonly ISystemPermissionCreateValidation _createValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemPermissionRepository _repository;

    public SystemPermissionCreateCoreHandler(ISystemPermissionCreateValidation createValidation,
        ISystemPermissionRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _createValidation = createValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemPermissionCreateCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _createValidation.Execute(request).ConfigureAwait(false);
        if (!result.Success)
        {
            return result;
        }

        HydrateValues(request);
        await _repository.Add(request).ConfigureAwait(false);
        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        await _repository.Add(request).ConfigureAwait(false);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);
        return new CreateResult<Entity>(true, BusinessMessage.MSG01);
    }

    private static void HydrateValues(SystemPermission target)
    {
        target.Name = target.Name.ToUpper(CultureInfo.CurrentCulture).Trim();
        target.Tag = target.Tag.ToUpper(CultureInfo.CurrentCulture).Trim();
    }
}
