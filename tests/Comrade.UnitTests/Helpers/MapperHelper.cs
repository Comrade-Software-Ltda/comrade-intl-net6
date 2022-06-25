using AutoMapper;
using Comrade.Application.Mappers;
using Comrade.Application.Paginations;

namespace Comrade.UnitTests.Helpers;

public static class MapperHelper
{
    public static IMapper ConfigMapper()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DtoToDomainMappingProfile());
            cfg.AddProfile(new DomainToDtoMappingProfile());
            cfg.AddProfile(new PaginationProfile());
        }).CreateMapper();
    }
}