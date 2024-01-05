using System.Threading;
using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.AirplaneCore.Handlers;

public class
    AirplaneEditCoreHandler(
        IAirplaneEditValidation airplaneEditValidation,
        IAirplaneRepository repository,
        IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<AirplaneEditCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(AirplaneEditCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await repository.GetById(request.Id);

        if (recordExists is null)
        {
            return new EditResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = await airplaneEditValidation.Execute(request, recordExists)
            ;

        if (!validate.Success)
        {
            return validate;
        }

        var obj = recordExists;
        HydrateValues(obj, request);

        await repository.BeginTransactionAsync();
        repository.Update(obj);
        await repository.CommitTransactionAsync();

        mongoDbContext.ReplaceOne(obj);

        return new EditResult<Entity>(true,
            BusinessMessage.MSG02);
    }

    private static void HydrateValues(Airplane target, Airplane source)
    {
        target.Code = source.Code;
        target.PassengerQuantity = source.PassengerQuantity;
        target.Model = source.Model;
    }
}
