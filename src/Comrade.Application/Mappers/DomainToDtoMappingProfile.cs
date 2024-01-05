using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.Airplane.Contracts;
using Comrade.Application.Components.Authentication.Contracts;
using Comrade.Application.Components.SystemMenu.Contracts;
using Comrade.Application.Components.SystemPermission.Contracts;
using Comrade.Application.Components.SystemRole.Contracts;
using Comrade.Application.Components.SystemUser.Contracts;
using Comrade.Application.Lookups;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Application.Mappers;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Entity, EntityDto>();
        CreateMap<Lookup, LookupDto>();
        CreateMap<Airplane, AirplaneDto>();
        CreateMap<SystemUser, SystemUserDto>();
        CreateMap<SystemUser, SystemUserWithPermissionsDto>();
        CreateMap<SystemUser, SystemUserWithRolesDto>();
        CreateMap<SystemUser, AuthenticationDto>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        CreateMap<SystemRole, SystemRoleDto>();
        CreateMap<SystemRole, SystemRoleWithPermissionsDto>();
        CreateMap<SystemMenu, SystemMenuDto>();
        CreateMap<SystemMenu, SystemMenuSimpleDto>();
        CreateMap<SystemPermission, SystemPermissionDto>();
    }
}
