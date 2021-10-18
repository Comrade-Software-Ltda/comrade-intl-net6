using Comrade.Core.Bases.Results;

namespace Comrade.Core.SecurityCore;

public interface IUcValidateLogin
{
    Task<SecurityResult> Execute(string key, string password);
}