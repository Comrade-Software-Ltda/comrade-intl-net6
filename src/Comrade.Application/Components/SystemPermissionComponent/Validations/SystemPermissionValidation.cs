using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Application.Messages;
using FluentValidation;

namespace Comrade.Application.Components.SystemPermissionComponent.Validations;

public class SystemPermissionValidation<TDto> : DtoValidation<TDto> where TDto : SystemPermissionDto
{
    protected void ValidateName()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Name");
    }

    protected void ValidateTag()
    {
        RuleFor(v => v.Tag)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Tag");
    }
}
