using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Application.Paginations;

namespace Comrade.Application.Components.SystemPermissionComponent.Queries;

public interface ISystemPermissionQuery
{
    Task<IPageResultDto<SystemPermissionDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemPermissionDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<SystemPermissionDto>> GetByIdMongo(Guid id);
}
