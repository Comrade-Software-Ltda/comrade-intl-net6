using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Extensions;
using MediatR;

namespace Comrade.Core.SystemUserCore.Handlers;

public class
    SystemUserCreateCoreHandler(
        ISystemUserCreateValidation systemUserCreateValidation,
        ISystemUserRepository repository,
        IMongoDbCommandContext mongoDbContext,
        IPasswordHasher passwordHasher)
    : IRequestHandler<SystemUserCreateCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbCommandContext _mongoDbContext = mongoDbContext;

    public async Task<ISingleResult<Entity>> Handle(SystemUserCreateCommand request,
        CancellationToken cancellationToken)
    {
        var validate = systemUserCreateValidation.Execute(request);
        if (!validate.Success)
        {
            return validate;
        }

        request.Password = passwordHasher.Hash(request.Password);
        request.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();

        await repository.Add(request);

        await repository.BeginTransactionAsync();
        await repository.Add(request);
        await repository.CommitTransactionAsync();

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }
}
