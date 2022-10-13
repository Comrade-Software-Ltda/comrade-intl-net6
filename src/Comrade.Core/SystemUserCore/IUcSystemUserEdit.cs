using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SystemUserCore;

public interface IUcSystemUserEdit
{
    Task<ISingleResult<Entity>> Execute(SystemUserEditCommand entity);
}
