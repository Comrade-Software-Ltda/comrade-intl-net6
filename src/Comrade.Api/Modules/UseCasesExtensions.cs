using Comrade.Application.Bases;
using Comrade.Application.Caches;
using Comrade.Application.Caches.FunctionCache;
using Comrade.Application.Components.AirplaneComponent.Commands;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Application.Components.AirplaneComponent.Handlers;
using Comrade.Application.Components.AirplaneComponent.Queries;
using Comrade.Application.Components.AuthenticationComponent.Commands;
using Comrade.Application.Components.FunctionComponent.Queries;
using Comrade.Application.Components.SystemMenuComponent.Commands;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Application.Components.SystemMenuComponent.Handlers;
using Comrade.Application.Components.SystemMenuComponent.Queries;
using Comrade.Application.Components.SystemUserComponent.Commands;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Application.Components.SystemUserComponent.Handlers;
using Comrade.Application.Components.SystemUserComponent.Queries;
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
using Comrade.Core.SystemMenuCore;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Core.SystemMenuCore.Handlers;
using Comrade.Core.SystemMenuCore.UseCases;
using Comrade.Core.SystemMenuCore.Validations;
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
        services.AddScoped<IAirplaneEditValidation, AirplaneEditValidation>();
        services.AddScoped<IAirplaneDeleteValidation, AirplaneDeleteValidation>();
        services.AddScoped<IAirplaneCreateValidation, AirplaneCreateValidation>();
        services.AddScoped<IAirplaneCodeUniqueValidation, AirplaneCodeUniqueValidation>();

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
        services
            .AddScoped<ISystemUserForgotPasswordValidation, SystemUserForgotPasswordValidation>();
        services.AddScoped<ISystemUserPasswordValidation, SystemUserPasswordValidation>();
        services.AddScoped<ISystemUserEditValidation, SystemUserEditValidation>();
        services.AddScoped<ISystemUserDeleteValidation, SystemUserDeleteValidation>();
        services.AddScoped<ISystemUserCreateValidation, SystemUserCreateValidation>();

        #endregion

        #region SystemMenu

        // Application - Services
        services.AddScoped<ISystemMenuCommand, SystemMenuCommand>();
        services.AddScoped<ISystemMenuQuery, SystemMenuQuery>();

        // Application - Handlers
        services
            .AddScoped<IRequestHandler<SystemMenuCreateDto, SingleResultDto<EntityDto>>,
                SystemMenuCreateHandler>();
        services
            .AddScoped<IRequestHandler<SystemMenuEditDto, SingleResultDto<EntityDto>>,
                SystemMenuEditHandler>();

        // Core - UseCases
        services.AddScoped<IUcSystemMenuEdit, UcSystemMenuEdit>();
        services.AddScoped<IUcSystemMenuCreate, UcSystemMenuCreate>();
        services.AddScoped<IUcSystemMenuDelete, UcSystemMenuDelete>();

        // Core - CoreHandlers
        services
            .AddScoped<IRequestHandler<SystemMenuCreateCommand, ISingleResult<Entity>>,
                SystemMenuCreateCoreHandler>();
        services
            .AddScoped<IRequestHandler<SystemMenuDeleteCommand, ISingleResult<Entity>>,
                SystemMenuDeleteCoreHandler>();
        services
            .AddScoped<IRequestHandler<SystemMenuEditCommand, ISingleResult<Entity>>,
                SystemMenuEditCoreHandler>();

        // Core - Validations
        services.AddScoped<ISystemMenuCreateValidation, SystemMenuCreateValidation>();
        services.AddScoped<ISystemMenuEditValidation, SystemMenuEditValidation>();
        services.AddScoped<SystemMenuDeleteValidation>();
        services.AddScoped<ISystemMenuUniqueValidation, SystemMenuUniqueValidation>();
        
        #endregion

        #region Alticci

        // Application - Services
        services.AddScoped<IAlticciQuery, AlticciQuery>();
        services.AddScoped<IRedisCacheService, RedisCacheService>();
        services.AddScoped<IRedisCacheFunctionService, RedisCacheFunctionService>();

        #endregion

        return services;
    }
}