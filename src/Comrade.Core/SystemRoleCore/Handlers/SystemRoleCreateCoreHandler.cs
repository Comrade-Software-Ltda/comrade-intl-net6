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

public class SystemRoleCreateCoreHandler(
    ISystemRoleCreateValidation createValidation,
    ISystemRoleRepository repository)
    : IRequestHandler<SystemRoleCreateCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemRoleCreateCommand request,
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

    private static void HydrateValues(SystemRole target)
    {
        target.Name = target.Name.ToUpper(CultureInfo.CurrentCulture).Trim();
        target.Tag = target.Tag.ToUpper(CultureInfo.CurrentCulture).Trim();
    }
}
