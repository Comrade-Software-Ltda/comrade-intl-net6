#region

using Comrade.Api.Modules;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Api.Modules.Common.Swagger;
using Comrade.Application.Lookups;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Extensions;
using Comrade.Persistence.Bases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

#endregion

namespace Comrade.UnitTests.Helpers;

public static class WebHostBuilderExt
{
    public static WebHostBuilder ConfigureServicesTest(this WebHostBuilder @this)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();

        var mockProvider = new Mock<IApiVersionDescriptionProvider>();
        mockProvider.Setup(foo => foo.ApiVersionDescriptions).Returns(
            new List<ApiVersionDescription>
            {
                    new(new ApiVersion(1, 0), "v1", false),
                    new(new ApiVersion(2, 0), "v2", false)
            });

        @this.ConfigureServices(services =>
            {
                services
                    .AddFeatureFlags(configuration)
                    .AddSqlServer(configuration)
                    .AddEntityRepository()
                    .AddHealthChecks(configuration)
                    .AddAuthentication(configuration)
                    .AddVersioning()
                    .AddSwagger()
                    .AddUseCases()
                    .AddCustomControllers()
                    .AddCustomCors()
                    .AddProxy()
                    .AddCustomDataProtection();

                services.AddAutoMapperSetup();

                services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

                services.AddScoped<IPasswordHasher, PasswordHasher>();
                services.AddScoped<HashingOptions>();
            })
            .Configure(builder =>
            {
                builder
                    .UseProxy(configuration)
                    .UseHealthChecks()
                    .UseCustomCors()
                    .UseCustomHttpMetrics()
                    .UseRouting()
                    .UseVersionedSwagger(mockProvider.Object, configuration)
                    .UseAuthentication()
                    .UseAuthorization()
                    .UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
            });

        return @this;
    }
}
