using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Application.Paginations;
using Comrade.Core.AirplaneCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Components.AirplaneComponent.Queries;

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

    public async Task<ISingleResultDto<AirplaneDto>> GetByIdDefault(Guid id)
    {
        var entity = await repository.GetById(id);
        var dto = mapper.Map<AirplaneDto>(entity);
        return new SingleResultDto<AirplaneDto>(dto);
    }

    public async Task<ISingleResultDto<AirplaneDto>> GetByIdMongo(Guid id)
    {
        var entity = await mongoDbQueryContext.GetById<Airplane?>(id);
        var dto = mapper.Map<AirplaneDto>(entity);
        return new SingleResultDto<AirplaneDto>(dto);
    }
}
