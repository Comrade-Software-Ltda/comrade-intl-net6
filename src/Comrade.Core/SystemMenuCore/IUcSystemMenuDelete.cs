using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemMenuCore;

public interface IUcSystemMenuDelete
{
    Task<ISingleResult<Entity>> Execute(Guid id);
}