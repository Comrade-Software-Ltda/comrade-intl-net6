﻿using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUser.Contracts;

public class SystemUserEditDto : SystemUserDto, IRequest<SingleResultDto<EntityDto>>
{
}
