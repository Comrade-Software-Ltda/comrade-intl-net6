using Comrade.Application.Bases;

namespace Comrade.Application.Components.FunctionComponent.Queries;

public interface IAlticciQuery
{
    FunctionReturnDto? CalculaAlticci(long n);
}
