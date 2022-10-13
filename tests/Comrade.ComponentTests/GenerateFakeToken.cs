using Comrade.Core.SecurityCore.Commands;
using Comrade.Core.SecurityCore.UseCases;
using MediatR;

namespace Comrade.ComponentTests;

public static class GenerateFakeToken
{
    public static async Task<string> Execute(IMediator mediator)
    {
        var ucGenerateToken = new UcGenerateToken(mediator);

        var roles = new List<string>
        {
            "Test"
        };

        var user = new GenerateTokenCommand
        {
            Id = new Guid(),
            Name = "Test",
            Token = "",
            Roles = roles
        };

        var token = await ucGenerateToken.Execute(user);
        return token;
    }
}
