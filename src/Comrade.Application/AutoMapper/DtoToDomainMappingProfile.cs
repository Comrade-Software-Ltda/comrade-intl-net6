using AutoMapper;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Application.Services.AuthenticationServices.Dtos;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Domain.Models;

namespace Comrade.Application.AutoMapper;

public class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile()
    {
        CreateMap<AirplaneDto, Airplane>();
        CreateMap<SystemUserDto, SystemUser>();
        CreateMap<AuthenticationDto, SystemUser>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
}