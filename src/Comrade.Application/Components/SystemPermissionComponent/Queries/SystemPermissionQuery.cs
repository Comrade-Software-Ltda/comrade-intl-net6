using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Application.Paginations;
using Comrade.Core.SystemPermissionCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Components.SystemPermissionComponent.Queries;

public class SystemPermissionQuery : ISystemPermissionQuery
{
    private readonly IMapper _mapper;
    private readonly IMongoDbQueryContext _mongoDbQueryContext;
    private readonly ISystemPermissionRepository _repository;

    public SystemPermissionQuery(ISystemPermissionRepository repository, IMongoDbQueryContext mongoDbQueryContext,
        IMapper mapper)
    {
        _repository = repository;
        _mongoDbQueryContext = mongoDbQueryContext;
        _mapper = mapper;
    }

    public async Task<IPageResultDto<SystemPermissionDto>> GetAll(PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = _mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<SystemPermissionDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<SystemPermissionDto>(_mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);
            return new PageResultDto<SystemPermissionDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemPermissionDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);
        return new PageResultDto<SystemPermissionDto>(paginationFilter, list);
    }

    public async Task<ISingleResultDto<SystemPermissionDto>> GetByIdDefault(Guid id)
    {
        var entity = await _repository.GetById(id).ConfigureAwait(false);
        var dto = _mapper.Map<SystemPermissionDto>(entity);
        return new SingleResultDto<SystemPermissionDto>(dto);
    }

    public async Task<ISingleResultDto<SystemPermissionDto>> GetByIdMongo(Guid id)
    {
        var entity = await _mongoDbQueryContext.GetById<SystemRole?>(id).ConfigureAwait(false);
        var dto = _mapper.Map<SystemPermissionDto>(entity);
        return new SingleResultDto<SystemPermissionDto>(dto);
    }
}
