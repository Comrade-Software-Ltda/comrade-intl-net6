namespace Comrade.Core.Bases.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Commit();
    Task<int> AffectedRows();
}
