using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore;

public interface IUcSystemUserEdit
{
    Task<ISingleResult<SystemUser>> Execute(SystemUser entity);
}