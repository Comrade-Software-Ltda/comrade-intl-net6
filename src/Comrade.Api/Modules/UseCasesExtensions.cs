using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Commands;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Application.Services.AirplaneServices.Handlers;
using Comrade.Application.Services.AirplaneServices.Queries;
using Comrade.Application.Services.AuthenticationServices.Commands;
using Comrade.Application.Services.SystemUserServices.Commands;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Application.Services.SystemUserServices.Handlers;
using Comrade.Application.Services.SystemUserServices.Queries;
using Comrade.Core.AirplaneCore;
using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.AirplaneCore.Handlers;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SecurityCore;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Core.SecurityCore.Handlers;
using Comrade.Core.SecurityCore.UseCases;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Core.SystemUserCore.Handlers;
using Comrade.Core.SystemUserCore.UseCases;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Api.Modules;

/// <summary>
///     Adds Use Cases classes.
/// </summary>
public static class UseCasesExtensions
{
    /// <summary>
    ///     Adds Use Cases to the ServiceCollection.
    /// </summary>
    /// <param name="services">CoreService Collection.</param>
    /// <returns>The modified instance.</returns>
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        #region Authentication

        // Application - Services
        services.AddScoped<IAuthenticationCommand, AuthenticationCommand>();

        // Core - UseCases
        services.AddScoped<IUcUpdatePassword, UcUpdatePassword>();
        services.AddScoped<IUcValidateLogin, UcValidateLogin>();
        services.AddScoped<IUcForgotPassword, UcForgotPassword>();
        services.AddScoped<IUcGenerateToken, UcGenerateToken>();

        // Core - CoreHandlers
        services
            .AddScoped<IRequestHandler<ForgotPasswordCommand, ISingleResult<Entity>>,
                ForgotPasswordCoreHandler>();
        services
            .AddScoped<IRequestHandler<GenerateTokenCommand, string>,
                GenerateTokenCoreHandler>();
        services
            .AddScoped<IRequestHandler<UpdatePasswordCommand, ISingleResult<Entity>>,
                UpdatePasswordCoreHandler>();

        #endregion

        #region Airplane

        // Application - Services
        services.AddScoped<IAirplaneCommand, AirplaneCommand>();
        services.AddScoped<IAirplaneQuery, AirplaneQuery>();

        // Application - ServiceHandlers
        services
            .AddScoped<IRequestHandler<AirplaneCreateDto, SingleResultDto<EntityDto>>,
                AirplaneCreateServiceHandler>();
        services
            .AddScoped<IRequestHandler<AirplaneEditDto, SingleResultDto<EntityDto>>,
                AirplaneEditServiceHandler>();

        // Core - UseCases
        services.AddScoped<IUcAirplaneEdit, UcAirplaneEdit>();
        services.AddScoped<IUcAirplaneCreate, UcAirplaneCreate>();
        services.AddScoped<IUcAirplaneDelete, UcAirplaneDelete>();

        // Core - CoreHandlers
        services
            .AddScoped<IRequestHandler<AirplaneCreateCommand, ISingleResult<Entity>>,
                AirplaneCreateCoreHandler>();
        services
            .AddScoped<IRequestHandler<AirplaneDeleteCommand, ISingleResult<Entity>>,
                AirplaneDeleteCoreHandler>();
        services
            .AddScoped<IRequestHandler<AirplaneEditCommand, ISingleResult<Entity>>,
                AirplaneEditCoreHandler>();

        // Core - Validations
        services.AddScoped<AirplaneEditValidation>();
        services.AddScoped<AirplaneDeleteValidation>();
        services.AddScoped<AirplaneCreateValidation>();
        services.AddScoped<AirplaneValidateSameCode>();

        #endregion

        #region SystemUser

        // Application - Services
        services.AddScoped<ISystemUserCommand, SystemUserCommand>();
        services.AddScoped<ISystemUserQuery, SystemUserQuery>();

        // Application - Handlers
        services
            .AddScoped<IRequestHandler<SystemUserCreateDto, SingleResultDto<EntityDto>>,
                SystemUserCreateHandler>();
        services
            .AddScoped<IRequestHandler<SystemUserEditDto, SingleResultDto<EntityDto>>,
                SystemUserEditHandler>();

        // Core - UseCases
        services.AddScoped<IUcSystemUserEdit, UcSystemUserEdit>();
        services.AddScoped<IUcSystemUserCreate, UcSystemUserCreate>();
        services.AddScoped<IUcSystemUserDelete, UcSystemUserDelete>();

        // Core - CoreHandlers
        services
            .AddScoped<IRequestHandler<SystemUserCreateCommand, ISingleResult<Entity>>,
                SystemUserCreateCoreHandler>();
        services
            .AddScoped<IRequestHandler<SystemUserDeleteCommand, ISingleResult<Entity>>,
                SystemUserDeleteCoreHandler>();
        services
            .AddScoped<IRequestHandler<SystemUserEditCommand, ISingleResult<Entity>>,
                SystemUserEditCoreHandler>();

        // Core - Validations
        services.AddScoped<SystemUserForgotPasswordValidation>();
        services.AddScoped<SystemUserPasswordValidation>();
        services.AddScoped<SystemUserEditValidation>();
        services.AddScoped<SystemUserDeleteValidation>();
        services.AddScoped<SystemUserCreateValidation>();

        #endregion

        return services;
    }
}