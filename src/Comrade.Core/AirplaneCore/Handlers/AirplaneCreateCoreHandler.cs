using System.Threading;
using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Extensions;
using MediatR;

namespace Comrade.Core.AirplaneCore.Handlers;

public class
    AirplaneCreateCoreHandler(
        IAirplaneCreateValidation airplaneCreateValidation,
        IAirplaneRepository repository,
        IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<AirplaneCreateCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(AirplaneCreateCommand request,
        CancellationToken cancellationToken)
    {
        var validate = await airplaneCreateValidation.Execute(request);
        if (!validate.Success)
        {
            return validate;
        }

        request.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();

        await repository.BeginTransactionAsync();
        await repository.Add(request);
        await repository.CommitTransactionAsync();

        mongoDbContext.InsertOne(request);

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }
}
