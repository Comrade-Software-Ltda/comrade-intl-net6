using Comrade.Core.SecurityCore.Commands;

namespace Comrade.Core.SecurityCore;

public interface IUcGenerateToken
{
    Task<string> Execute(GenerateTokenCommand entity);
}