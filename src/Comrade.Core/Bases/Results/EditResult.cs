using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;

namespace Comrade.Core.Bases.Results;

public class EditResult<TEntity> : SingleResult<TEntity>
    where TEntity : Entity
{
    public EditResult()
    {
        Code = (int) EnumResponse.NoContent;
        Success = true;
        Message = BusinessMessage.MSG02;
    }

    public EditResult(bool success, string? message)
    {
        Code = success ? (int) EnumResponse.NoContent : (int) EnumResponse.NotFound;
        Success = success;
        Message = message;
    }
}
