namespace Comrade.Application.Bases.Interfaces;

public interface ISingleResultDto<out TDto> : IResultDto
    where TDto : Dto
{
    TDto? Data { get; }
}
