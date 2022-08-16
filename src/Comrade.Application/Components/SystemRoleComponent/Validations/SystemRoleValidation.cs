using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Application.Messages;
using FluentValidation;

namespace Comrade.Application.Components.SystemRoleComponent.Validations;

public class SystemRoleValidation<TDto> : DtoValidation<TDto> where TDto : SystemRoleDto
{
    protected void ValidateName()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Name");
    }
}
