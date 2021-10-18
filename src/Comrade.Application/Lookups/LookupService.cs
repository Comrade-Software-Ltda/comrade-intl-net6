using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Application.Lookups;

public class LookupService<TEntity> : Service, ILookupService<TEntity>
    where TEntity : Entity
{
    private readonly IRepository<TEntity> _repository;

    public LookupService(IRepository<TEntity> repository, IMapper mapper)
        : base(mapper)
    {
        _repository = repository;
    }

    public async Task<List<LookupDto>> GetLookup()
    {
        var list = await Task.Run(() => _repository.GetLookup()
            .ProjectTo<LookupDto>(Mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return list.OrderBy(x => x.Value).ToList();
    }

    public async Task<List<LookupDto>> GetLookup(Expression<Func<TEntity, bool>> predicate)
    {
        var list = await Task.Run(() => _repository.GetLookup(predicate)
            .ProjectTo<LookupDto>(Mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return list;
    }
}