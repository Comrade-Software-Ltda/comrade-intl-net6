using AutoMapper;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Application.Components.AuthenticationComponent.Contracts;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Models;

namespace Comrade.Application.Mappers;

public class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile()
    {
        CreateMap<AirplaneDto, Airplane>();
        CreateMap<AirplaneCreateDto, AirplaneCreateCommand>();
        CreateMap<AirplaneEditDto, AirplaneEditCommand>();
        CreateMap<SystemUserDto, SystemUser>();
        CreateMap<SystemUserDto, SystemUserCreateCommand>();
        CreateMap<SystemUserDto, SystemUserEditCommand>();
        CreateMap<AuthenticationDto, SystemUser>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        CreateMap<AuthenticationDto, UpdatePasswordCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        CreateMap<AuthenticationDto, ForgotPasswordCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        CreateMap<SystemMenuDto, SystemMenu>();
        CreateMap<SystemMenuSimpleDto, SystemMenu>();
        CreateMap<SystemMenuCreateDto, SystemMenuCreateCommand>();
        CreateMap<SystemMenuCreateDto, SystemMenu>();
        CreateMap<SystemMenuEditDto, SystemMenuEditCommand>();
        CreateMap<SystemMenuDeleteDto, SystemMenuDeleteCommand>();
    }
}