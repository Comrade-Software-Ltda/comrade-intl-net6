using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;

namespace Comrade.Core.Bases.Results;

public class DeleteResult<TEntity> : SingleResult<TEntity>
    where TEntity : Entity
{
    public DeleteResult()
    {
        Code = (int) EnumResponse.Ok;
        Success = true;
        Message = BusinessMessage.MSG03;
    }

    public DeleteResult(bool success, string? message)
    {
        Code = success ? (int) EnumResponse.Ok : (int) EnumResponse.NotFound;
        Success = success;
        Message = message;
    }
}
