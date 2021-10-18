using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Paginations;
using Comrade.Core.Messages;
using Comrade.Domain.Enums;

namespace Comrade.Application.Bases;

public class PageResultDto<T> : ResultDto, IPageResultDto<T>
    where T : Dto
{
    public PageResultDto(List<T>? data)
    {
        Data = data;
        Code = data == null ? (int)EnumResponse.ErrorNotFound : (int)EnumResponse.Success;
        Success = data != null;
        Message = data == null
            ? BusinessMessage.ResourceManager.GetString("MSG04", CultureInfo.CurrentCulture)
            : string.Empty;
    }

    public PageResultDto(PaginationFilter pagination, List<T>? data)
    {
        Data = data;
        PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : null;
        PageSize = pagination.PageNumber >= 1 ? pagination.PageSize : null;
        NextPage = pagination.PageNumber + 1;
        PreviousPage = pagination.PageNumber > 1 ? pagination.PageNumber - 1 : null;
        Code = data == null ? (int)EnumResponse.ErrorNotFound : (int)EnumResponse.Success;
        Success = data != null;
        Message = data == null
            ? BusinessMessage.ResourceManager.GetString("MSG04", CultureInfo.CurrentCulture)
            : string.Empty;
    }

    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public int? NextPage { get; set; }
    public int? PreviousPage { get; set; }
    public IList<T>? Data { get; set; }
}