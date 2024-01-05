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

public class SystemPermissionCreateCoreHandler(
    ISystemPermissionCreateValidation createValidation,
    ISystemPermissionRepository repository,
    IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<SystemPermissionCreateCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbCommandContext _mongoDbContext = mongoDbContext;

    public async Task<ISingleResult<Entity>> Handle(SystemPermissionCreateCommand request,
        CancellationToken cancellationToken)
    {
        var result = await createValidation.Execute(request);
        if (!result.Success)
        {
            return result;
        }

        HydrateValues(request);
        await repository.Add(request);
        await repository.BeginTransactionAsync();
        await repository.Add(request);
        await repository.CommitTransactionAsync();
        return new CreateResult<Entity>(true, BusinessMessage.MSG01);
    }

    private static void HydrateValues(SystemPermission target)
    {
        target.Name = target.Name.ToUpper(CultureInfo.CurrentCulture).Trim();
        target.Tag = target.Tag.ToUpper(CultureInfo.CurrentCulture).Trim();
    }
}
