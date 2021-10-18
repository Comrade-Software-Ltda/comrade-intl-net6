using Comrade.Domain.Token;

namespace Comrade.Core.SecurityCore;

public interface IUcGenerateToken
{
    string Execute(TokenUser tokenUser);
}