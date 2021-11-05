using Comrade.Domain.Token;
using MediatR;

namespace Comrade.Core.SecurityCore.Commands;

public class GenerateTokenCommand : TokenUser, IRequest<string>
{
}