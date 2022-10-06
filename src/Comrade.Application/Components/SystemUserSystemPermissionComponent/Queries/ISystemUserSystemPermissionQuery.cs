using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserSystemPermissionComponent.Contracts;
using Comrade.Application.Paginations;

namespace Comrade.Application.Components.SystemUserSystemPermissionComponent.Queries;

public interface ISystemUserSystemPermissionQuery
{
    Task<IPageResultDto<SystemUserSystemPermissionDto>> GetAll(PaginationQuery? paginationQuery = null);
}