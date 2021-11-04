using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Token;
using MediatR;

namespace Comrade.Core.SecurityCore.Commands;

public class GenerateTokenCommand : TokenUser, IRequest<string>, IRequest<ISingleResult<Entity>>
{
}