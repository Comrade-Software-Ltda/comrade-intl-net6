using Swashbuckle.AspNetCore.SwaggerGen;

namespace Comrade.Api.Modules.Common.Swagger;

/// <summary>
///     Configures the Swagger generation options.
/// </summary>
/// <remarks>
///     This allows API versioning to define a Swagger document per API version after the
///     <see cref="IApiVersionDescriptionProvider" /> service has been resolved from the service container.
/// </remarks>
/// <remarks>
///     Initializes a new instance of the <see cref="ConfigureSwaggerOptions" /> class.
/// </remarks>
/// <param name="provider">
///     The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger
///     documents.
/// </param>
public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    private const string UriString = "http://urlaqui.net";

    private const string UriString1 =
        "https://raw.urlaqui.com/README.md";

    private readonly IApiVersionDescriptionProvider _provider = provider;

    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        // add a swagger document for each discovered API version
        // note: you might choose to skip or document deprecated API versions differently
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        OpenApiInfo info = new()
        {
            Title = "Clean Architecture Comrade",
            Version = description.ApiVersion.ToString(),
            Description = "",
            Contact = new OpenApiContact {Name = "name aqui", Email = "email@aqui"},
            TermsOfService = new Uri(UriString),
            License = new OpenApiLicense
            {
                Name = "Apache License",
                Url = new Uri(
                    UriString1)
            }
        };

        if (description.IsDeprecated)
            info.Description += " This API version has been deprecated.";

        return info;
    }
}
