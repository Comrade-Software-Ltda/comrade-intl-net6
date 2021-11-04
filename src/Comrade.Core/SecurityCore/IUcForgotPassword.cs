using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Domain.Bases;

namespace Comrade.Core.SecurityCore;

public interface IUcForgotPassword
{
    Task<ISingleResult<Entity>> Execute(ForgotPasswordCommand entity);
}