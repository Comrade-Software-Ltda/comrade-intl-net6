using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.SecurityCore;

public interface IUcUpdatePassword
{
    Task<ISingleResult<SystemUser>> Execute(SystemUser entity);
}