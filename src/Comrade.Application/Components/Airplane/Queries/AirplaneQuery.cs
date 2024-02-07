using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.Airplane.Contracts;
using Comrade.Application.Pagination;
using Comrade.Core.AirplaneCore;
using Comrade.Core.Bases.Interfaces;

namespace Comrade.Application.Components.Airplane.Queries;

public class AirplaneQuery(
    IAirplaneRepository repository,
    IMongoDbQueryContext mongoDbQueryContext,
    IMapper mapper)
    : IAirplaneQuery
{
    public async Task<IPageResultDto<AirplaneDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<AirplaneDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => repository.GetAllAsNoTracking()
                .ProjectTo<AirplaneDto>(mapper.ConfigurationProvider)
                .ToList());

            return new PageResultDto<AirplaneDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<AirplaneDto>(mapper.ConfigurationProvider)
            .ToList());

        return new PageResultDto<AirplaneDto>(paginationFilter, list);
    }

    public IPageResultDto<AirplaneDto> GetByProjection(PaginationSearchQuery paginationSearchQuery)
    {
        var result = repository.GetByProjection(paginationSearchQuery.PropertyName, paginationSearchQuery.SearchValue);

        if (!result.Any())
        {
            return new PageResultDto<AirplaneDto>("error the projection don`t generate any results");
        }

        var skip = (paginationSearchQuery.PageNumber - 1) * paginationSearchQuery.PageSize;
        var list = result
            .ProjectTo<AirplaneDto>(mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(paginationSearchQuery.PageSize)
            .ToList();

        return new PageResultDto<AirplaneDto>(list);
    }

    public async Task<ISingleResultDto<AirplaneDto>> GetByIdDefault(Guid id)
    {
        var entity = await repository.GetById(id);
        var dto = mapper.Map<AirplaneDto>(entity);
        return new SingleResultDto<AirplaneDto>(dto);
    }

    public async Task<ISingleResultDto<AirplaneDto>> GetByIdMongo(Guid id)
    {
        var entity = await mongoDbQueryContext.GetById<Domain.Models.Airplane?>(id);
        var dto = mapper.Map<AirplaneDto>(entity);
        return new SingleResultDto<AirplaneDto>(dto);
    }
}
