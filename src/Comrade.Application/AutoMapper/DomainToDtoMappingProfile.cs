using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.AirplaneComponent.Dtos;
using Comrade.Application.Components.AuthenticationComponent.Dtos;
using Comrade.Application.Components.SystemUserComponent.Dtos;
using Comrade.Application.Lookups;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Application.AutoMapper;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Entity, EntityDto>();
        CreateMap<Lookup, LookupDto>();
        CreateMap<Airplane, AirplaneDto>();
        CreateMap<SystemUser, SystemUserDto>();
        CreateMap<SystemUser, AuthenticationDto>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
}