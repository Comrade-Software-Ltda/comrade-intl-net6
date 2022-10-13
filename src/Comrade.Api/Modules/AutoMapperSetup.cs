using Comrade.Application.Mappers;

namespace Comrade.Api.Modules;

public static class AutoMapperSetup
{
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(typeof(DomainToDtoMappingProfile),
            typeof(DtoToDomainMappingProfile));
    }
}
