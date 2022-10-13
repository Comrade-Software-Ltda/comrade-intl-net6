using AutoMapper;
using Comrade.Application.Paginations;

namespace Comrade.Application.Mappers;

public static class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DomainToDtoMappingProfile());
            cfg.AddProfile(new DtoToDomainMappingProfile());
            cfg.AddProfile(new PaginationProfile());
        });
    }
}
