using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.SecurityCore;

public interface IUcForgotPassword
{
    Task<ISingleResult<SystemUser>> Execute(SystemUser entity);
}