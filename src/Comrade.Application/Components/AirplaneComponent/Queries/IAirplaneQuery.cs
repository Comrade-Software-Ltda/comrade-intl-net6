using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Application.Paginations;

namespace Comrade.Application.Components.AirplaneComponent.Queries;

public interface IAirplaneQuery
{
    Task<IPageResultDto<AirplaneDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<AirplaneDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<AirplaneDto>> GetByIdMongo(Guid id);
}