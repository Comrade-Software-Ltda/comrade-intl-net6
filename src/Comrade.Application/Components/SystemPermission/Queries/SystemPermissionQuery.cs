using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemPermission.Contracts;
using Comrade.Application.Pagination;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemPermissionCore;

namespace Comrade.Application.Components.SystemPermission.Queries;

public class SystemPermissionQuery(
    ISystemPermissionRepository repository,
    IMongoDbQueryContext mongoDbQueryContext,
    IMapper mapper)
    : ISystemPermissionQuery
{
    public async Task<IPageResultDto<SystemPermissionDto>> GetAll(PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<SystemPermissionDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => repository.GetAllAsNoTracking()
                .ProjectTo<SystemPermissionDto>(mapper.ConfigurationProvider)
                .ToList());
            return new PageResultDto<SystemPermissionDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        list = await Task.Run(() => repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemPermissionDto>(mapper.ConfigurationProvider)
            .ToList());
        return new PageResultDto<SystemPermissionDto>(paginationFilter, list);
    }

    public async Task<ISingleResultDto<SystemPermissionDto>> GetByIdDefault(Guid id)
    {
        var entity = await repository.GetById(id);
        var dto = mapper.Map<SystemPermissionDto>(entity);
        return new SingleResultDto<SystemPermissionDto>(dto);
    }

    public async Task<ISingleResultDto<SystemPermissionDto>> GetByIdMongo(Guid id)
    {
        var entity = await mongoDbQueryContext.GetById<Domain.Models.SystemRole?>(id);
        var dto = mapper.Map<SystemPermissionDto>(entity);
        return new SingleResultDto<SystemPermissionDto>(dto);
    }
}
