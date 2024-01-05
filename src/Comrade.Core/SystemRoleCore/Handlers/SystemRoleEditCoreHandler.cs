using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Core.SystemRoleCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemRoleCore.Handlers;

public class SystemRoleEditCoreHandler(
    ISystemRoleEditValidation editValidation,
    ISystemRoleRepository repository,
    IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<SystemRoleEditCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemRoleEditCommand request, CancellationToken cancellationToken)
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

        HydrateValues(recordExists, request);
        repository.Update(recordExists);

        await repository.BeginTransactionAsync();
        repository.Update(recordExists);
        await repository.CommitTransactionAsync();

        mongoDbContext.ReplaceOne(recordExists);

        return new EditResult<Entity>(true, BusinessMessage.MSG02);
    }

    private static void HydrateValues(SystemRole target, SystemRole source)
    {
        target.Name = source.Name.ToUpper(CultureInfo.CurrentCulture).Trim();
        target.Tag = source.Tag.ToUpper(CultureInfo.CurrentCulture).Trim();
    }
}
