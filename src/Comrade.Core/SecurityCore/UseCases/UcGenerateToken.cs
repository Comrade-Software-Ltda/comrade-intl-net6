using Comrade.Core.SecurityCore.Commands;
using MediatR;

namespace Comrade.Core.SecurityCore.UseCases;

public class UcGenerateToken(IMediator mediator) : IUcGenerateToken
{
    public async Task<string> Execute(GenerateTokenCommand entity)
    {
        try
        {
            return await mediator.Send(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
