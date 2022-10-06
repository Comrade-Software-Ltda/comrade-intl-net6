using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.Application.Paginations;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Queries;

public interface ISystemUserSystemRoleQuery
{
    Task<IPageResultDto<SystemUserSystemRoleDto>> GetAll(PaginationQuery? paginationQuery = null);
}