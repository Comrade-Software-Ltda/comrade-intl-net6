#region


#endregion

namespace Comrade.Core.Bases.Interfaces;

public interface IUseCase
{
    Task<bool> Commit();
}
