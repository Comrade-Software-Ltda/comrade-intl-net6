#region

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Paginations;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Core.AirplaneCore;

#endregion

namespace Comrade.Application.Services.AirplaneServices.Queries;

public class AirplaneQuery : Service, IAirplaneQuery
{
    private readonly IAirplaneRepository _repository;

    public AirplaneQuery(IAirplaneRepository repository,
        IMapper mapper)
        : base(mapper)
    {
        _repository = repository;
    }

    public async Task<IPageResultDto<AirplaneDto>> GetAll(
        PaginationFilter? paginationFilter = null)
    {
        List<AirplaneDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<AirplaneDto>(Mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<AirplaneDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<AirplaneDto>(Mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new PageResultDto<AirplaneDto>(list);
    }

    public async Task<ISingleResultDto<AirplaneDto>> GetById(int id)
    {
        var entity = await _repository.GetById(id).ConfigureAwait(false);
        var dto = Mapper.Map<AirplaneDto>(entity);
        return new SingleResultDto<AirplaneDto>(dto);
    }
}
