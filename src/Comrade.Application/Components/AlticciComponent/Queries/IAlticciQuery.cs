using Comrade.Application.Bases;

namespace Comrade.Application.Components.AlticciComponent.Queries;

public interface IAlticciQuery
{
    AlticciDto CalculaAlticci(int n);
}
