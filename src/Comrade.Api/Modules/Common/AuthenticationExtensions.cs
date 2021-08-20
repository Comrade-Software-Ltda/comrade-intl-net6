#region

using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Core.SecurityCore;
using Comrade.Core.SecurityCore.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using System.Text;

#endregion

namespace Comrade.Api.Modules.Common;

/// <summary>
///     Authentication Extensions.
/// </summary>
public static class AuthenticationExtensions
{
    /// <summary>
    ///     Add Authentication Extensions.
    /// </summary>
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        IFeatureManager featureManager = services
            .BuildServiceProvider()
            .GetRequiredService<IFeatureManager>();

        var isEnabled = featureManager
            .IsEnabledAsync(nameof(CustomFeature.Authentication))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        if (isEnabled)
        {
            services.AddScoped<IUcValidateLogin, UcValidateLogin>();
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["jwt:key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        return services;
    }
}
