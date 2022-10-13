using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Application.Lookups;
using Comrade.Application.Paginations;
using Comrade.Core.SystemRoleCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Components.SystemRoleComponent.Queries;

public class SystemRoleQuery : ISystemRoleQuery
{
    private readonly IMapper _mapper;
    private readonly IMongoDbQueryContext _mongoDbQueryContext;
    private readonly ISystemRoleRepository _repository;

    public SystemRoleQuery(ISystemRoleRepository repository, IMongoDbQueryContext mongoDbQueryContext, IMapper mapper)
    {
        _repository = repository;
        _mongoDbQueryContext = mongoDbQueryContext;
        _mapper = mapper;
    }

    public async Task<IPageResultDto<SystemRoleDto>> GetAll(PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = _mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<SystemRoleDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<SystemRoleDto>(_mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);
            return new PageResultDto<SystemRoleDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemRoleDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);
        return new PageResultDto<SystemRoleDto>(paginationFilter, list);
    }

    public async Task<ListResultDto<LookupDto>> FindByName(string name)
    {
        var list = await Task.Run(() => _repository.FindByName(name)
            .ProjectTo<LookupDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);
        return new ListResultDto<LookupDto>(list);
    }

    public async Task<ISingleResultDto<SystemRoleDto>> GetByIdDefault(Guid id)
    {
        var entity = await _repository.GetById(id).ConfigureAwait(false);
        var dto = _mapper.Map<SystemRoleDto>(entity);
        return new SingleResultDto<SystemRoleDto>(dto);
    }

    public async Task<ISingleResultDto<SystemRoleDto>> GetByIdMongo(Guid id)
    {
        var entity = await _mongoDbQueryContext.GetById<SystemRole?>(id).ConfigureAwait(false);
        var dto = _mapper.Map<SystemRoleDto>(entity);
        return new SingleResultDto<SystemRoleDto>(dto);
    }
}
