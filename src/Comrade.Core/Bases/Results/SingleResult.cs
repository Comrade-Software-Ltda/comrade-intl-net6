using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Messages;
using Comrade.Domain.Bases.Interfaces;
using Comrade.Domain.Enums;

namespace Comrade.Core.Bases.Results;

public class SingleResult<TEntity> : ISingleResult<TEntity>
    where TEntity : IEntity
{
    public SingleResult()
    {
        Code = (int) EnumResponse.Ok;
        Success = true;
    }

    public SingleResult(IEnumerable<string> messages)
    {
        Code = (int) EnumResponse.BadRequest;
        Success = false;
        Messages = messages;
    }


    public SingleResult(int code, string? message)
    {
        Code = code;
        Success = false;
        Message = message ?? string.Empty;
    }

    public SingleResult(TEntity? data)
    {
        Code = data == null ? (int) EnumResponse.NotFound : (int) EnumResponse.Ok;
        Success = data != null;
        Message = data == null
            ? BusinessMessage.MSG04
            : string.Empty;
        Data = data;
    }

    public string? ExceptionMessage { get; set; }
    public IEnumerable<string>? Messages { get; set; }
    public int Code { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public TEntity? Data { get; set; }
}
