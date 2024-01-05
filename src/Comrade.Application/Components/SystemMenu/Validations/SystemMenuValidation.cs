using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Application.Messages;
using FluentValidation;

namespace Comrade.Application.Components.SystemMenuComponent.Validations;

public class SystemMenuValidation<TDto> : DtoValidation<TDto>
    where TDto : SystemMenuDto
{
    protected void ValidateTitle()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(30).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Text");
    }

    protected void ValidateDescription()
    {
        RuleFor(v => v.Description)
            .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
            .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Description");
    }

    protected void ValidateRoute()
    {
        RuleFor(v => v.Route)
            .Must(s => Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out _))
            .WithMessage(ApplicationMessage.URL_INVALIDA)
            .MaximumLength(255)
            .WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
            .WithName("Route");
    }
}
