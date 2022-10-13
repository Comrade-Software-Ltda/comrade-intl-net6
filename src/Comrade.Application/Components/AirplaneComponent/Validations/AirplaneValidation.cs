using Comrade.Application.Bases;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Application.Messages;
using FluentValidation;

namespace Comrade.Application.Components.AirplaneComponent.Validations;

public class AirplaneValidation<TDto> : DtoValidation<TDto>
    where TDto : AirplaneDto
{
    protected void ValidateCode()
    {
        RuleFor(v => v.Code)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Code");
    }

    protected void ValidateModel()
    {
        RuleFor(v => v.Model)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Model");
    }

    protected void ValidatePassengerQuantity()
    {
        RuleFor(v => v.PassengerQuantity)
            .NotNull().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .GreaterThan(0).WithMessage(ApplicationMessage.CAMPO_MAIOR_QUE_ZERO)
            .WithName("PassengerQuantity");
    }
}
