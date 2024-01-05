using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemRole.Contracts;
using Comrade.Application.Lookups;
using Comrade.Application.Paginations;
using Comrade.Core.SystemRoleCore;

namespace Comrade.Application.Components.SystemRole.Queries;

public class SystemRoleQuery(ISystemRoleRepository repository, IMongoDbQueryContext mongoDbQueryContext, IMapper mapper)
    : ISystemRoleQuery
{
    public async Task<IPageResultDto<SystemRoleDto>> GetAll(PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<SystemRoleDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => repository.GetAllAsNoTracking()
                .ProjectTo<SystemRoleDto>(mapper.ConfigurationProvider)
                .ToList());
            return new PageResultDto<SystemRoleDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        list = await Task.Run(() => repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemRoleDto>(mapper.ConfigurationProvider)
            .ToList());
        return new PageResultDto<SystemRoleDto>(paginationFilter, list);
    }

    public async Task<IPageResultDto<SystemRoleWithPermissionsDto>> GetAllWithPermissions(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);

        List<SystemRoleWithPermissionsDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => repository.GetAllAsNoTracking()
                .ProjectTo<SystemRoleWithPermissionsDto>(mapper.ConfigurationProvider)
                .ToList());

            return new PageResultDto<SystemRoleWithPermissionsDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemRoleWithPermissionsDto>(mapper.ConfigurationProvider)
            .ToList());

        return new PageResultDto<SystemRoleWithPermissionsDto>(paginationFilter, list);
    }


    public async Task<ListResultDto<LookupDto>> FindByName(string name)
    {
        var list = await Task.Run(() => repository.FindByName(name)
            .ProjectTo<LookupDto>(mapper.ConfigurationProvider)
            .ToList());
        return new ListResultDto<LookupDto>(list);
    }

    public async Task<ISingleResultDto<SystemRoleDto>> GetByIdDefault(Guid id)
    {
        var entity = await repository.GetById(id);
        var dto = mapper.Map<SystemRoleDto>(entity);
        return new SingleResultDto<SystemRoleDto>(dto);
    }

    public async Task<ISingleResultDto<SystemRoleDto>> GetByIdMongo(Guid id)
    {
        var entity = await mongoDbQueryContext.GetById<Domain.Models.SystemRole?>(id);
        var dto = mapper.Map<SystemRoleDto>(entity);
        return new SingleResultDto<SystemRoleDto>(dto);
    }
}
