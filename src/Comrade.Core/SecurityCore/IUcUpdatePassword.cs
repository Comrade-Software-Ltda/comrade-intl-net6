using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SecurityCore;

public interface IUcUpdatePassword
{
    Task<ISingleResult<Entity>> Execute(UpdatePasswordCommand entity);
}
