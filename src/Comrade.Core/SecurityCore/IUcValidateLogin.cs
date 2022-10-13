using Comrade.Core.Bases.Results;

namespace Comrade.Core.SecurityCore;

public interface IUcValidateLogin
{
    Task<SecurityResult> Execute(Guid key, string password);
}
