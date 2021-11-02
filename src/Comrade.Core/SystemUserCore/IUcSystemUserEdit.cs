using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore;

public interface IUcSystemUserEdit
{
    Task<ISingleResult<Entity>> Execute(SystemUser entity);
}