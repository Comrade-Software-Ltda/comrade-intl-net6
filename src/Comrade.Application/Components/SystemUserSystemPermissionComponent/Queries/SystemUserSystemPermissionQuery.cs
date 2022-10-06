using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserSystemPermissionComponent.Contracts;
using Comrade.Application.Paginations;
using Comrade.Core.SystemUserCore;

namespace Comrade.Application.Components.SystemUserSystemPermissionComponent.Queries;

public class SystemUserSystemPermissionQuery : ISystemUserSystemPermissionQuery
{
    private readonly IMapper _mapper;
    private readonly ISystemUserRepository _repository;

    public SystemUserSystemPermissionQuery(ISystemUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IPageResultDto<SystemUserSystemPermissionDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = _mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);

        List<SystemUserSystemPermissionDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<SystemUserSystemPermissionDto>(_mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<SystemUserSystemPermissionDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemUserSystemPermissionDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new PageResultDto<SystemUserSystemPermissionDto>(paginationFilter, list);
    }
}