using Comrade.Domain.Bases;

namespace Comrade.Core.Bases.Interfaces;

public interface IMongoDbQueryContext
{
    IQueryable<T> GetAll<T>() where T : Entity;
    Task<T> GetById<T>(Guid id) where T : Entity?;
}
