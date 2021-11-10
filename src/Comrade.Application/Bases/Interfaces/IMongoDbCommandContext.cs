namespace Comrade.Application.Bases.Interfaces;

public interface IMongoDbQueryContext
{
    IQueryable<T> GetAll<T>() where T : EntityDto;
}