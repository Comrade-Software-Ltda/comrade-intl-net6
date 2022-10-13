using FluentValidation;

namespace Comrade.Application.Bases;

public class DtoValidation<TDto> : AbstractValidator<TDto>
    where TDto : EntityDto
{
}
