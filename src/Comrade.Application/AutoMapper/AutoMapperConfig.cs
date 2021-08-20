#region

using AutoMapper;
using Comrade.Application.Paginations;

#endregion

namespace Comrade.Application.AutoMapper;

public static class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new(cfg =>
        {
            cfg.AddProfile(new DomainToDtoMappingProfile());
            cfg.AddProfile(new DtoToDomainMappingProfile());
            cfg.AddProfile(new PaginationProfile());
        });
    }
}

