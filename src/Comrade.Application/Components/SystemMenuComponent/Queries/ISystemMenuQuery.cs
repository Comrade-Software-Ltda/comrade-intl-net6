using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Application.Paginations;

namespace Comrade.Application.Components.SystemMenuComponent.Queries;

public interface ISystemMenuQuery
{
    Task<IPageResultDto<SystemMenuDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemMenuDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<SystemMenuDto>> GetByIdMongo(Guid id);
}