using Comrade.Application.Bases;
using Comrade.Application.Messages;
using Comrade.Application.Services.SystemUserComponent.Dtos;
using FluentValidation;

namespace Comrade.Application.Services.SystemUserComponent.Validations;

public class SystemUserValidation<TDto> : DtoValidation<TDto>
    where TDto : SystemUserDto
{
    protected void ValidateName()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Name");
    }

    protected void ValidateEmail()
    {
        RuleFor(v => v.Email)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Email");
    }

    protected void ValidateRegistration()
    {
        RuleFor(v => v.Registration)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Registration");
    }
}