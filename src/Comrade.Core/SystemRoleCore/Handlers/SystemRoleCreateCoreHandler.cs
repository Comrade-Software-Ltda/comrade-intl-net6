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

public class SystemRoleCreateCoreHandler : IRequestHandler<SystemRoleCreateCommand, ISingleResult<Entity>>
{
    private readonly ISystemRoleCreateValidation _createValidation;
    private readonly ISystemRoleRepository _repository;

    public SystemRoleCreateCoreHandler(ISystemRoleCreateValidation createValidation,
        ISystemRoleRepository repository)
    {
        _createValidation = createValidation;
        _repository = repository;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemRoleCreateCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _createValidation.Execute(request);
        if (!result.Success)
        {
            return result;
        }

        HydrateValues(request);

        await _repository.Add(request);
        await _repository.BeginTransactionAsync();
        await _repository.Add(request);
        await _repository.CommitTransactionAsync();
        return new CreateResult<Entity>(true, BusinessMessage.MSG01);
    }

    private static void HydrateValues(SystemRole target)
    {
        target.Name = target.Name.ToUpper(CultureInfo.CurrentCulture).Trim();
        target.Tag = target.Tag.ToUpper(CultureInfo.CurrentCulture).Trim();
    }
}
