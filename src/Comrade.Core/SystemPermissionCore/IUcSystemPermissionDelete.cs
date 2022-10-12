using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemPermissionCore;

public interface IUcSystemPermissionDelete
{
    Task<ISingleResult<Entity>> Execute(Guid id);
}