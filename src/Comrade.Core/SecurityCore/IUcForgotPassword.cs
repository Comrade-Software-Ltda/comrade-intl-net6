#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SecurityCore;

public interface IUcForgotPassword
{
    Task<ISingleResult<SystemUser>> Execute(SystemUser entity);
}
