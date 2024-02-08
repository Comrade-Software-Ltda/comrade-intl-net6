using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUser.Contracts;
using Comrade.Application.Lookups;
using Comrade.Application.Pagination;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore;

namespace Comrade.Application.Components.SystemUser.Queries;

public class SystemUserQuery(
    ISystemUserRepository repository,
    IMongoDbQueryContext mongoDbQueryContext,
    IMapper mapper)
    : ISystemUserQuery
{
    public async Task<IPageResultDto<SystemUserDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);

        List<SystemUserDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => repository.GetAllAsNoTracking()
                .ProjectTo<SystemUserDto>(mapper.ConfigurationProvider)
                .ToList());

            return new PageResultDto<SystemUserDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemUserDto>(mapper.ConfigurationProvider)
            .ToList());

        return new PageResultDto<SystemUserDto>(paginationFilter, list);
    }

    public async Task<ListResultDto<LookupDto>> FindByName(string name)
    {
        var list = await Task.Run(() => repository.FindByName(name)
            .ProjectTo<LookupDto>(mapper.ConfigurationProvider)
            .ToList());

        return new ListResultDto<LookupDto>(list);
    }

    public async Task<ISingleResultDto<SystemUserDto>> GetByIdDefault(Guid id)
    {
        var entity = await repository.GetById(id);
        var dto = mapper.Map<SystemUserDto>(entity);
        return new SingleResultDto<SystemUserDto>(dto);
    }

    public async Task<ISingleResultDto<SystemUserDto>> GetByIdMongo(Guid id)
    {
        var entity = await mongoDbQueryContext.GetById<Domain.Models.SystemUser?>(id);
        var dto = mapper.Map<SystemUserDto>(entity);
        return new SingleResultDto<SystemUserDto>(dto);
    }

    public async Task<IPageResultDto<SystemUserWithPermissionsDto>> GetAllWithPermissions(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);

        List<SystemUserWithPermissionsDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => repository.GetAllAsNoTracking()
                .ProjectTo<SystemUserWithPermissionsDto>(mapper.ConfigurationProvider)
                .ToList());

            return new PageResultDto<SystemUserWithPermissionsDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemUserWithPermissionsDto>(mapper.ConfigurationProvider)
            .ToList());

        return new PageResultDto<SystemUserWithPermissionsDto>(paginationFilter, list);
    }

    public async Task<IPageResultDto<SystemUserWithRolesDto>> GetAllWithRoles(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);

        List<SystemUserWithRolesDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => repository.GetAllAsNoTracking()
                .ProjectTo<SystemUserWithRolesDto>(mapper.ConfigurationProvider)
                .ToList());

            return new PageResultDto<SystemUserWithRolesDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemUserWithRolesDto>(mapper.ConfigurationProvider)
            .ToList());

        return new PageResultDto<SystemUserWithRolesDto>(paginationFilter, list);
    }
}
