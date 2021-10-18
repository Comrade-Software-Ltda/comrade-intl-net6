using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore;

public interface IUcSystemUserDelete
{
    Task<ISingleResult<SystemUser>> Execute(int id);
}