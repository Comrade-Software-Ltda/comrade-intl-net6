using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemMenuCore;

public interface IUcSystemMenuCreate
{
    Task<ISingleResult<Entity>> Execute(SystemMenuCreateCommand entity);
}
