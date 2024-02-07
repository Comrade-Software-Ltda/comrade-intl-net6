using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemPermission.Contracts;
using Comrade.Application.Pagination;

namespace Comrade.Application.Components.SystemPermission.Queries;

public interface ISystemPermissionQuery
{
    Task<IPageResultDto<SystemPermissionDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemPermissionDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<SystemPermissionDto>> GetByIdMongo(Guid id);
}
