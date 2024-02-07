using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemMenu.Contracts;
using Comrade.Application.Pagination;
using Comrade.Core.SystemMenuCore;

namespace Comrade.Application.Components.SystemMenu.Queries;

public class SystemMenuQuery(
    ISystemMenuRepository repository,
    IMongoDbQueryContext mongoDbQueryContext,
    IMapper mapper)
    : ISystemMenuQuery
{
    public async Task<IPageResultDto<SystemMenuSimpleDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<SystemMenuSimpleDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => repository.GetAllAsNoTracking()
                .ProjectTo<SystemMenuSimpleDto>(mapper.ConfigurationProvider)
                .ToList());

            return new PageResultDto<SystemMenuSimpleDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemMenuSimpleDto>(mapper.ConfigurationProvider)
            .ToList());

        return new PageResultDto<SystemMenuSimpleDto>(paginationFilter, list);
    }

    public async Task<IPageResultDto<SystemMenuDto>> GetAllMenus(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<SystemMenuDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => repository.GetAllMenus()
                .ProjectTo<SystemMenuDto>(mapper.ConfigurationProvider)
                .ToList());

            return new PageResultDto<SystemMenuDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => repository.GetAllMenus().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemMenuDto>(mapper.ConfigurationProvider)
            .ToList());

        return new PageResultDto<SystemMenuDto>(paginationFilter, list);
    }

    public async Task<ISingleResultDto<SystemMenuDto>> GetByIdDefault(Guid id)
    {
        var entity = await repository.GetById(id);
        var dto = mapper.Map<SystemMenuDto>(entity);
        return new SingleResultDto<SystemMenuDto>(dto);
    }

    public async Task<ISingleResultDto<SystemMenuDto>> GetByIdMongo(Guid id)
    {
        var entity = await mongoDbQueryContext.GetById<Domain.Models.SystemMenu?>(id);
        var dto = mapper.Map<SystemMenuDto>(entity);
        return new SingleResultDto<SystemMenuDto>(dto);
    }
}
