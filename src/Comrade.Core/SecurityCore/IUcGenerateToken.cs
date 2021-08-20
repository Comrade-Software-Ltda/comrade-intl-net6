#region

using Comrade.Domain.Token;

#endregion

namespace Comrade.Core.SecurityCore;

public interface IUcGenerateToken
{
    string Execute(TokenUser tokenUser);
}
