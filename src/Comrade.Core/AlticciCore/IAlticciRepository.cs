using Comrade.Domain.Models;

namespace Comrade.Core.AlticciCore;

public interface IAlticciRepository
{
    Alticci GetFromCacheIfExist(int n);
    void AddCache(Alticci alticci);
}
