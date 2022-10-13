using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.SecurityCore.Validation;

public interface ISystemUserPasswordValidation
{
    ISingleResult<SystemUser> Execute(Guid key, string password);
}
