namespace Comrade.Api.Modules.Common.FeatureFlags;

/// <summary>
///     Features Flags Enum.
/// </summary>
public enum CustomFeature
{
    /// <summary>
    ///     Get Airplane.
    /// </summary>
    Airplane,

    /// <summary>
    ///     Get SystemPermission.
    /// </summary>
    SystemPermission,

    /// <summary>
    ///     Get SystemRole.
    /// </summary>
    SystemRole,

    /// <summary>
    ///     Get SystemRoleSystemPermission.
    /// </summary>
    SystemRoleSystemPermission,

    /// <summary>
    ///     Get Alticci.
    /// </summary>
    Alticci,

    /// <summary>
    ///     Get SystemUser.
    /// </summary>
    SystemUser,

    /// <summary>
    ///     Get SystemMenu.
    /// </summary>
    SystemMenu,

    /// <summary>
    ///     Get TenantSelector.
    /// </summary>
    TenantSelector,

    /// <summary>
    ///     Get Common.
    /// </summary>
    Common,

    /// <summary>
    ///     HealthChecks.
    /// </summary>
    HealthChecks,

    /// <summary>
    ///     Use Swagger.
    /// </summary>
    Swagger,

    /// <summary>
    ///     Use Ms SQL Server Persistence.
    /// </summary>
    MsSqlServer,

    /// <summary>
    ///     Use Postgres Sql Persistence.
    /// </summary>
    PostgresSql,


    /// <summary>
    ///     Inject Initial Data on memory Db.
    /// </summary>
    InjectInitialData,


    /// <summary>
    ///     Use authentication.
    /// </summary>
    Authentication
}
