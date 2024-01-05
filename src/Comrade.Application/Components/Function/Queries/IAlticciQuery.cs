using Comrade.Application.Bases;

namespace Comrade.Application.Components.Function.Queries;

public interface IAlticciQuery
{
    FunctionReturnDto? CalculaAlticci(long n);
}
