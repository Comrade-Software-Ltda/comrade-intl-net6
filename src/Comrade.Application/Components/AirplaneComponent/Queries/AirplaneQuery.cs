using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Application.Paginations;
using Comrade.Core.AirplaneCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Components.AirplaneComponent.Queries;

public class AirplaneQuery : IAirplaneQuery
{
    private readonly IMapper _mapper;
    private readonly IMongoDbQueryContext _mongoDbQueryContext;
    private readonly IAirplaneRepository _repository;

    public AirplaneQuery(IAirplaneRepository repository,
        IMongoDbQueryContext mongoDbQueryContext, IMapper mapper)
    {
        _repository = repository;
        _mongoDbQueryContext = mongoDbQueryContext;
        _mapper = mapper;
    }

    public async Task<IPageResultDto<AirplaneDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = _mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<AirplaneDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<AirplaneDto>(_mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<AirplaneDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<AirplaneDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new PageResultDto<AirplaneDto>(paginationFilter, list);
    }

    public async Task<ISingleResultDto<AirplaneDto>> GetByIdDefault(Guid id)
    {
        var entity = await _repository.GetById(id).ConfigureAwait(false);
        var dto = _mapper.Map<AirplaneDto>(entity);
        return new SingleResultDto<AirplaneDto>(dto);
    }

    public async Task<ISingleResultDto<AirplaneDto>> GetByIdMongo(Guid id)
    {
        var entity = await _mongoDbQueryContext.GetById<Airplane?>(id).ConfigureAwait(false);
        var dto = _mapper.Map<AirplaneDto>(entity);
        return new SingleResultDto<AirplaneDto>(dto);
    }
}
