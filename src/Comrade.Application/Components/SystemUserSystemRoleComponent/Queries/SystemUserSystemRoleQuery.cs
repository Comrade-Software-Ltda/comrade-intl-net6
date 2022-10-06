using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.Application.Paginations;
using Comrade.Core.SystemUserCore;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Queries;

public class SystemUserSystemRoleQuery : ISystemUserSystemRoleQuery
{
    private readonly IMapper _mapper;
    private readonly ISystemUserRepository _repository;

    public SystemUserSystemRoleQuery(ISystemUserRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IPageResultDto<SystemUserSystemRoleDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = _mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);

        List<SystemUserSystemRoleDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<SystemUserSystemRoleDto>(_mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<SystemUserSystemRoleDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemUserSystemRoleDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new PageResultDto<SystemUserSystemRoleDto>(paginationFilter, list);
    }
}