namespace Comrade.Application.Bases.Interfaces;

public interface IListResultDto<TDto> : IResultDto
    where TDto : Dto
{
    List<TDto>? Data { get; set; }
}
