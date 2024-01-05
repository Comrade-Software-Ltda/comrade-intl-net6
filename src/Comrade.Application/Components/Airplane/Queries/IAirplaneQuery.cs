using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.Airplane.Contracts;
using Comrade.Application.Paginations;

namespace Comrade.Application.Components.Airplane.Queries;

public interface IAirplaneQuery
{
    Task<IPageResultDto<AirplaneDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<AirplaneDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<AirplaneDto>> GetByIdMongo(Guid id);
}
