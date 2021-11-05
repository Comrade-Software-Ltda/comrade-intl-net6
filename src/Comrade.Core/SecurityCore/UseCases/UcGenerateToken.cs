using Comrade.Core.SecurityCore.Commands;
using MediatR;

namespace Comrade.Core.SecurityCore.UseCases;

public class UcGenerateToken : IUcGenerateToken
{
    private readonly IMediator _mediator;

    public UcGenerateToken(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<string> Execute(GenerateTokenCommand entity)
    {
        try
        {
            return await _mediator.Send(entity).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}