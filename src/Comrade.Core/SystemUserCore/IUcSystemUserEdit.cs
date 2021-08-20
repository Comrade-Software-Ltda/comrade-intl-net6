#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore;

public interface IUcSystemUserEdit
{
    Task<ISingleResult<SystemUser>> Execute(SystemUser entity);
}
