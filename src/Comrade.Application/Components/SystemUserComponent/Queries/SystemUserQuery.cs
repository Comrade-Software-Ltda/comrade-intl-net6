using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Application.Lookups;
using Comrade.Application.Paginations;
using Comrade.Core.SystemUserCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Components.SystemUserComponent.Queries;

public class SystemUserQuery : ISystemUserQuery
{
    private readonly IMapper _mapper;
    private readonly IMongoDbQueryContext _mongoDbQueryContext;
    private readonly ISystemUserRepository _repository;

    public SystemUserQuery(ISystemUserRepository repository,
        IMongoDbQueryContext mongoDbQueryContext, IMapper mapper)
    {
        _repository = repository;
        _mongoDbQueryContext = mongoDbQueryContext;
        _mapper = mapper;
    }

    public async Task<IPageResultDto<SystemUserDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = _mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);

        List<SystemUserDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<SystemUserDto>(_mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<SystemUserDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemUserDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new PageResultDto<SystemUserDto>(paginationFilter, list);
    }

    public async Task<ListResultDto<LookupDto>> FindByName(string name)
    {
        var list = await Task.Run(() => _repository.FindByName(name)
            .ProjectTo<LookupDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new ListResultDto<LookupDto>(list);
    }

    public async Task<ISingleResultDto<SystemUserDto>> GetByIdDefault(Guid id)
    {
        var entity = await _repository.GetById(id).ConfigureAwait(false);
        var dto = _mapper.Map<SystemUserDto>(entity);
        return new SingleResultDto<SystemUserDto>(dto);
    }

    public async Task<ISingleResultDto<SystemUserDto>> GetByIdMongo(Guid id)
    {
        var entity = await _mongoDbQueryContext.GetById<SystemUser?>(id).ConfigureAwait(false);
        var dto = _mapper.Map<SystemUserDto>(entity);
        return new SingleResultDto<SystemUserDto>(dto);
    }
}