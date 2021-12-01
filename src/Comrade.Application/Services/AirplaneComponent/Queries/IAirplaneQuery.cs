using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Paginations;
using Comrade.Application.Services.AirplaneServices.Dtos;

namespace Comrade.Application.Services.AirplaneServices.Queries;

public interface IAirplaneQuery
{
    Task<IPageResultDto<AirplaneDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<AirplaneDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<AirplaneDto>> GetByIdMongo(Guid id);
}