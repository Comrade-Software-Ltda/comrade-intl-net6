using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Lookups;
using Comrade.Application.Paginations;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Core.SystemUserCore;
using Comrade.Domain.Bases;

namespace Comrade.Application.Services.SystemUserServices.Queries;

public class SystemUserQuery : Service, ISystemUserQuery
{
    private readonly ISystemUserRepository _repository;

    public SystemUserQuery(ISystemUserRepository repository,
        IMapper mapper)
        : base(mapper)
    {
        _repository = repository;
    }

    public async Task<IPageResultDto<SystemUserDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = Mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);

        List<SystemUserDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<SystemUserDto>(Mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<SystemUserDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<SystemUserDto>(Mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new PageResultDto<SystemUserDto>(paginationFilter, list);
    }

    public async Task<ListResultDto<LookupDto>> FindByName(string name)
    {
        var success = int.TryParse(name, out var number);
        List<LookupDto> list = new();

        if (success)
        {
            var entity = await _repository.GetById(number).ConfigureAwait(false);
            if (entity != null)
            {
                var dto = Mapper.Map<LookupDto>(new Lookup
                { Key = entity.Id, Value = entity.Name });
                list = new List<LookupDto> { dto };
            }
        }
        else if (!string.IsNullOrEmpty(name))
        {
            list = await Task.Run(() => _repository.FindByName(name)
                .ProjectTo<LookupDto>(Mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);
        }

        return new ListResultDto<LookupDto>(list);
    }


    public async Task<ISingleResultDto<SystemUserDto>> GetById(int id)
    {
        var entity = await _repository.GetById(id).ConfigureAwait(false);
        var dto = Mapper.Map<SystemUserDto>(entity);
        return new SingleResultDto<SystemUserDto>(dto);
    }
}