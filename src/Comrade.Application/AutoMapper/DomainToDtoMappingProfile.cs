using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Lookups;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Application.Services.AuthenticationServices.Dtos;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Application.AutoMapper;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Entity, EntityDto>();
        CreateMap<Lookup, LookupDto>();

        CreateMap<Airplane, AirplaneEditDto>();
        CreateMap<Airplane, AirplaneDto>();

        CreateMap<SystemUser, SystemUserEditDto>();
        CreateMap<SystemUser, SystemUserDto>();

        CreateMap<SystemUser, AuthenticationDto>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
}