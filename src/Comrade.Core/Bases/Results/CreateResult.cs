using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;

namespace Comrade.Core.Bases.Results;

public class CreateResult<TEntity> : SingleResult<TEntity>
    where TEntity : Entity
{
    public CreateResult()
    {
        Code = (int) EnumResponse.Created;
        Success = true;
        Message = BusinessMessage.MSG01;
    }

    public CreateResult(bool success, string? message)
    {
        Code = success ? (int) EnumResponse.Created : (int) EnumResponse.NotFound;
        Success = success;
        Message = message;
    }
}
