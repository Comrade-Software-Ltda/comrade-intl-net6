using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemUserCore;

public interface IUcSystemUserCreate
{
    Task<ISingleResult<Entity>> Execute(SystemUserCreateCommand entity);
}
