using Comrade.Application.Bases;
using Comrade.Core.AlticciCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Components.AlticciComponent.Queries;

public class AlticciQuery : IAlticciQuery
{
    private readonly IAlticciRepository _repository;

    public AlticciQuery(IAlticciRepository repository)
    {
        _repository = repository;
    }

    public AlticciDto CalculaAlticci(int n)
    {
        AlticciDto result;
        Alticci entity = _repository.GetFromCacheIfExist(n);
        if (entity == null)
        {
            result = new AlticciDto(n, CalculaAlticciFunc(n));
            entity = new Alticci   (n, result.AlticciAn);
            _repository.AddCache(entity);
        }
        else
        {
            result = new AlticciDto(n, entity.AlticciAn);
        }
        return result;
    }

    private int? CalculaAlticciFunc(int n)
    {
        if (n <= 0)
        {
            return 0;
        }
        if (n == 1 || n == 2)
        {
            return 1;
        }
        Alticci entity = _repository.GetFromCacheIfExist(n);
        if (entity != null)
        {
            return entity.AlticciAn;
        }
        return CalculaAlticciFunc(n - 3) + CalculaAlticciFunc(n - 2);
    }
}
