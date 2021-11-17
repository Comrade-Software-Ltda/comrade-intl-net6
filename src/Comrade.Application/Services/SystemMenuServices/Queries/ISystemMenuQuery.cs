using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Paginations;
using Comrade.Application.Services.SystemMenuServices.Dtos;

namespace Comrade.Application.Services.SystemMenuServices.Queries;

public interface ISystemMenuQuery
{
    Task<IPageResultDto<SystemMenuDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemMenuDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<SystemMenuDto>> GetByIdMongo(Guid id);
}