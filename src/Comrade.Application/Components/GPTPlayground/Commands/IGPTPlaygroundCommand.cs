namespace Comrade.Application.Components.GPTPlayground.Commands;

public interface IGPTPlaygroundCommand
{
    Task<dynamic> Create();
}
