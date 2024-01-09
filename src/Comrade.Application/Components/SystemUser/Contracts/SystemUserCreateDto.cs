using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUser.Contracts;

public class SystemUserCreateDto : SystemUserDto, IRequest<SingleResultDto<EntityDto>>
{
}
