﻿using AutoMapper;
using Comrade.Application.Components.Airplane.Contracts;
using Comrade.Application.Components.Authentication.Contracts;
using Comrade.Application.Components.SystemMenu.Contracts;
using Comrade.Application.Components.SystemPermission.Contracts;
using Comrade.Application.Components.SystemRole.Contracts;
using Comrade.Application.Components.SystemUser.Contracts;
using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Core.SystemRoleCore.Commands;
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
        CreateMap<SystemUserManagePermissionsDto, SystemUserManagePermissionsCommand>();
        CreateMap<SystemUserManageRolesDto, SystemUserManageRolesCommand>();
        CreateMap<SystemRoleDto, SystemRole>();
        CreateMap<SystemRoleCreateDto, SystemRoleCreateCommand>();
        CreateMap<SystemRoleEditDto, SystemRoleEditCommand>();
        CreateMap<SystemRoleManagePermissionsDto, SystemRoleManagePermissionsCommand>();
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
        CreateMap<SystemPermissionDto, SystemPermission>();
        CreateMap<SystemPermissionCreateDto, SystemPermissionCreateCommand>();
        CreateMap<SystemPermissionEditDto, SystemPermissionEditCommand>();
    }
}
