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
    ///     Get SystemUser.
    /// </summary>
    SystemUser,

    /// <summary>
    ///     Get Common.
    /// </summary>
    Common,

    /// <summary>
    ///     Filter errors out.
    /// </summary>
    ErrorFilter,

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
    ///     Use authentication.
    /// </summary>
    Authentication
}
