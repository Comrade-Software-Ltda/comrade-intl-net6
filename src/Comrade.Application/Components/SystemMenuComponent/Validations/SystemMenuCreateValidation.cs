using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Application.Messages;
using FluentValidation;

namespace Comrade.Application.Components.SystemMenuComponent.Validations;

public class SystemMenuCreateValidation : AbstractValidator<SystemMenuCreateDto>
{
    public SystemMenuCreateValidation()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(30).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Text");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Description");

        RuleFor(v => v.Route)
            .Must(s => Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out _))
            .WithMessage(ApplicationMessage.URL_INVALIDA)
            .MaximumLength(255)
            .WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Route");
    }
}
