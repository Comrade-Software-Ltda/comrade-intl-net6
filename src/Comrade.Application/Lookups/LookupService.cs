using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Application.Lookups;

public class LookupService<TEntity> : ILookupService<TEntity>
    where TEntity : Entity
{
    private readonly IMapper _mapper;
    private readonly IRepository<TEntity> _repository;

    public LookupService(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<LookupDto>> GetLookup()
    {
        var list = await Task.Run(() => _repository.GetLookup()
            .ProjectTo<LookupDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return list.OrderBy(x => x.Value).ToList();
    }

    public async Task<List<LookupDto>> GetLookup(Expression<Func<TEntity, bool>> predicate)
    {
        var list = await Task.Run(() => _repository.GetLookup(predicate)
            .ProjectTo<LookupDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return list;
    }
}
