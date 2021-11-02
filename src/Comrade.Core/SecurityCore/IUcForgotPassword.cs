using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SecurityCore;

public interface IUcForgotPassword
{
    Task<ISingleResult<Entity>> Execute(SystemUser entity);
}