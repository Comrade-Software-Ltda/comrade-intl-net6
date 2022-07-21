using Comrade.Core.AlticciCore;
using Comrade.Domain.Models;

namespace Comrade.Persistence.Repositories;

public class AlticciRepository : IAlticciRepository
{
    private HashSet<Alticci> cache = new();

    public AlticciRepository() {}

    public Alticci GetFromCacheIfExist(int n)
    {
        foreach (Alticci elem in cache)
        {
            if (elem.AlticciN == n)
            {
                return elem;
            }
        }
        return null;
    }

    public void AddCache(Alticci alticci)
    {
        if (alticci != null)
        {
            foreach (Alticci elem in cache)
            {
                if (elem?.AlticciN == alticci.AlticciN)
                {
                    return;
                }
            }
            cache.Add(alticci);
        }
    }
}
