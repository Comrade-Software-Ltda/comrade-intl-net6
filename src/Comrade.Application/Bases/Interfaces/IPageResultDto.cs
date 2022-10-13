namespace Comrade.Application.Bases.Interfaces;

public interface IPageResultDto<TDto> : IResultDto
    where TDto : Dto
{
    IList<TDto>? Data { get; set; }
}
