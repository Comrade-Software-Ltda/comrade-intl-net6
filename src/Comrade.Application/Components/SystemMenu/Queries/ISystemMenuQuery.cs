using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemMenu.Contracts;
using Comrade.Application.Pagination;

namespace Comrade.Application.Components.SystemMenu.Queries;

public interface ISystemMenuQuery
{
    Task<IPageResultDto<SystemMenuSimpleDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<IPageResultDto<SystemMenuDto>> GetAllMenus(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemMenuDto>> GetByIdDefault(Guid id);
}
