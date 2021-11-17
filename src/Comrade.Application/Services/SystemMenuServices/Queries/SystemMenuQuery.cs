using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Paginations;
using Comrade.Application.Services.SystemMenuServices.Dtos;
using Comrade.Core.SystemMenuCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Services.SystemMenuServices.Queries;

public class SystemMenuQuery : ISystemMenuQuery
{
    private readonly IMapper _mapper;
    private readonly IMongoDbQueryContext _mongoDbQueryContext;
    private readonly ISystemMenuRepository _repository;

    public SystemMenuQuery(ISystemMenuRepository repository,
        IMongoDbQueryContext mongoDbQueryContext, IMapper mapper)
    {
        _repository = repository;
        _mongoDbQueryContext = mongoDbQueryContext;
        _mapper = mapper;
    }

    public async Task<IPageResultDto<SystemMenuDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = _mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<SystemMenuDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<SystemMenuDto>(_mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<SystemMenuDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemMenuDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new PageResultDto<SystemMenuDto>(paginationFilter, list);
    }

    public async Task<ISingleResultDto<SystemMenuDto>> GetByIdDefault(Guid id)
    {
        var entity = await _repository.GetById(id).ConfigureAwait(false);
        var dto = _mapper.Map<SystemMenuDto>(entity);
        return new SingleResultDto<SystemMenuDto>(dto);
    }

    public async Task<ISingleResultDto<SystemMenuDto>> GetByIdMongo(Guid id)
    {
        var entity = await _mongoDbQueryContext.GetById<SystemMenu?>(id).ConfigureAwait(false);
        var dto = _mapper.Map<SystemMenuDto>(entity);
        return new SingleResultDto<SystemMenuDto>(dto);
    }
}