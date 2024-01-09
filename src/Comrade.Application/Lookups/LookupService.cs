using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Application.Lookups;

public class LookupService<TEntity>(IRepository<TEntity> repository, IMapper mapper) : ILookupService<TEntity>
    where TEntity : Entity
{
    public async Task<List<LookupDto>> GetLookup()
    {
        var list = await Task.Run(() => repository.GetLookup()
            .ProjectTo<LookupDto>(mapper.ConfigurationProvider)
            .ToList());

        return list.OrderBy(x => x.Value).ToList();
    }

    public async Task<List<LookupDto>> GetLookup(Expression<Func<TEntity, bool>> predicate)
    {
        var list = await Task.Run(() => repository.GetLookup(predicate)
            .ProjectTo<LookupDto>(mapper.ConfigurationProvider)
            .ToList());

        return list;
    }
}
