using Comrade.Application.Bases.Interfaces;
using Comrade.Core.Messages;
using Comrade.Domain.Enums;

namespace Comrade.Application.Bases;

public class ListResultDto<T> : ResultDto, IListResultDto<T>
    where T : Dto
{
    public ListResultDto(List<T>? data)
    {
        Data = data;
        Code = data == null ? (int) EnumResponse.NotFound : (int) EnumResponse.Ok;
        Success = data != null;
        Message = data == null
            ? BusinessMessage.MSG04
            : string.Empty;
    }

    public List<T>? Data { get; set; }
}
