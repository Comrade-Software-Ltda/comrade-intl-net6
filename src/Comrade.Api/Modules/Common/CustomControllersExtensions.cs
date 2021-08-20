#region

using Comrade.Api.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.FeatureManagement;
using System.Text.Json;
using System.Text.Json.Serialization;

#endregion

namespace Comrade.Api.Modules.Common;

/// <summary>
///     Custom Controller Extensions.
/// </summary>
public static class CustomControllersExtensions
{
    /// <summary>
    ///     Add Custom Controller dependencies.
    /// </summary>
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        IFeatureManager featureManager = services
            .BuildServiceProvider()
            .GetRequiredService<IFeatureManager>();

        var isErrorFilterEnabled = featureManager
            .IsEnabledAsync(nameof(CustomFeature.ErrorFilter))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        services
            .AddHttpContextAccessor()
            .AddMvc(options =>
            {
                options.Conventions.Add(
                    new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                options.OutputFormatters.RemoveType<TextOutputFormatter>();
                options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                options.RespectBrowserAcceptHeader = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            })
            .AddControllersAsServices();

        return services;
    }
}
