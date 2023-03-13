﻿using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Application.Components.AuthenticationComponent.Contracts;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Application.Components.SystemUserComponent.Contracts;
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
