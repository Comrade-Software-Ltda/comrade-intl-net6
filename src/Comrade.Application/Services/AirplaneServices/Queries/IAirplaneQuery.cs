using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Paginations;
using Comrade.Application.Services.AirplaneServices.Dtos;

namespace Comrade.Application.Services.AirplaneServices.Queries;

public interface IAirplaneQuery : IService
{
    Task<IPageResultDto<AirplaneDto>> GetAll(PaginationFilter? paginationFilter = null);
    Task<ISingleResultDto<AirplaneDto>> GetById(int id);
}