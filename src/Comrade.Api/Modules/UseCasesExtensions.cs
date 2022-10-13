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
using Comrade.Application.Components.SystemPermissionComponent.Commands;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Application.Components.SystemPermissionComponent.Handlers;
using Comrade.Application.Components.SystemPermissionComponent.Queries;
using Comrade.Application.Components.SystemRoleComponent.Commands;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Application.Components.SystemRoleComponent.Handlers;
using Comrade.Application.Components.SystemRoleComponent.Queries;
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
using Comrade.Core.SystemPermissionCore;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Core.SystemPermissionCore.Handlers;
using Comrade.Core.SystemPermissionCore.UseCases;
using Comrade.Core.SystemPermissionCore.Validations;
using Comrade.Core.SystemRoleCore;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Core.SystemRoleCore.Handlers;
using Comrade.Core.SystemRoleCore.UseCases;
using Comrade.Core.SystemRoleCore.Validations;
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

        #region SystemRole

        // Application - Services
        services.AddScoped<ISystemRoleCommand, SystemRoleCommand>();
        services.AddScoped<ISystemRoleQuery, SystemRoleQuery>();

        // Application - Handlers
        services.AddScoped<IRequestHandler<SystemRoleCreateDto, SingleResultDto<EntityDto>>, SystemRoleCreateHandler>();
        services.AddScoped<IRequestHandler<SystemRoleEditDto, SingleResultDto<EntityDto>>, SystemRoleEditHandler>();

        // Core - UseCases
        services.AddScoped<IUcSystemRoleEdit, UcSystemRoleEdit>();
        services.AddScoped<IUcSystemRoleCreate, UcSystemRoleCreate>();
        services.AddScoped<IUcSystemRoleDelete, UcSystemRoleDelete>();

        // Core - CoreHandlers
        services
            .AddScoped<IRequestHandler<SystemRoleCreateCommand, ISingleResult<Entity>>, SystemRoleCreateCoreHandler>();
        services
            .AddScoped<IRequestHandler<SystemRoleDeleteCommand, ISingleResult<Entity>>, SystemRoleDeleteCoreHandler>();
        services.AddScoped<IRequestHandler<SystemRoleEditCommand, ISingleResult<Entity>>, SystemRoleEditCoreHandler>();

        // Core - Validations
        services.AddScoped<ISystemRoleEditValidation, SystemRoleEditValidation>();
        services.AddScoped<ISystemRoleDeleteValidation, SystemRoleDeleteValidation>();
        services.AddScoped<ISystemRoleCreateValidation, SystemRoleCreateValidation>();
        services.AddScoped<ISystemRoleNameUniqueValidation, SystemRoleNameUniqueValidation>();

        #endregion

        #region SystemPermission

        // Application - Services
        services.AddScoped<ISystemPermissionCommand, SystemPermissionCommand>();
        services.AddScoped<ISystemPermissionQuery, SystemPermissionQuery>();

        // Application - Handlers
        services
            .AddScoped<IRequestHandler<SystemPermissionCreateDto, SingleResultDto<EntityDto>>,
                SystemPermissionCreateHandler>();
        services
            .AddScoped<IRequestHandler<SystemPermissionEditDto, SingleResultDto<EntityDto>>,
                SystemPermissionEditHandler>();

        // Core - UseCases
        services.AddScoped<IUcSystemPermissionEdit, UcSystemPermissionEdit>();
        services.AddScoped<IUcSystemPermissionCreate, UcSystemPermissionCreate>();
        services.AddScoped<IUcSystemPermissionDelete, UcSystemPermissionDelete>();

        // Core - CoreHandlers
        services
            .AddScoped<IRequestHandler<SystemPermissionCreateCommand, ISingleResult<Entity>>,
                SystemPermissionCreateCoreHandler>();
        services
            .AddScoped<IRequestHandler<SystemPermissionDeleteCommand, ISingleResult<Entity>>,
                SystemPermissionDeleteCoreHandler>();
        services
            .AddScoped<IRequestHandler<SystemPermissionEditCommand, ISingleResult<Entity>>,
                SystemPermissionEditCoreHandler>();

        // Core - Validations
        services.AddScoped<ISystemPermissionEditValidation, SystemPermissionEditValidation>();
        services.AddScoped<ISystemPermissionDeleteValidation, SystemPermissionDeleteValidation>();
        services.AddScoped<ISystemPermissionCreateValidation, SystemPermissionCreateValidation>();
        services.AddScoped<ISystemPermissionTagUniqueValidation, SystemPermissionTagUniqueValidation>();

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
        services
            .AddScoped<IRequestHandler<SystemUserManagePermissionsDto, SingleResultDto<EntityDto>>,
                SystemUserManagePermissionsHandler>();
        services
            .AddScoped<IRequestHandler<SystemUserManageRolesDto, SingleResultDto<EntityDto>>,
                SystemUserManageRolesHandler>();


        // Core - UseCases
        services.AddScoped<IUcSystemUserEdit, UcSystemUserEdit>();
        services.AddScoped<IUcSystemUserCreate, UcSystemUserCreate>();
        services.AddScoped<IUcSystemUserDelete, UcSystemUserDelete>();
        services.AddScoped<IUcSystemUserManagePermissions, UcSystemUserManagePermissions>();
        services.AddScoped<IUcSystemUserManageRoles, UcSystemUserManageRoles>();

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
        services
            .AddScoped<IRequestHandler<SystemUserManagePermissionsCommand, ISingleResult<Entity>>,
                SystemUserManagePermissionsCoreHandler>();
        services
            .AddScoped<IRequestHandler<SystemUserManageRolesCommand, ISingleResult<Entity>>,
                SystemUserManageRolesCoreHandler>();


        // Core - Validations
        services
            .AddScoped<ISystemUserForgotPasswordValidation, SystemUserForgotPasswordValidation>();
        services.AddScoped<ISystemUserPasswordValidation, SystemUserPasswordValidation>();
        services.AddScoped<ISystemUserEditValidation, SystemUserEditValidation>();
        services.AddScoped<ISystemUserDeleteValidation, SystemUserDeleteValidation>();
        services.AddScoped<ISystemUserCreateValidation, SystemUserCreateValidation>();
        services.AddScoped<ISystemUserManagePermissionsValidation, SystemUserManagePermissionsValidation>();
        services.AddScoped<ISystemUserManageRolesValidation, SystemUserManageRolesValidation>();

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
