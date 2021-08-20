#region

using Comrade.Core.Bases.Results;

#endregion

namespace Comrade.Core.SecurityCore;

public interface IUcValidateLogin
{
    Task<SecurityResult> Execute(string key, string password);
}
