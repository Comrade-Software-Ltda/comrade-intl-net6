using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.Airplane.Contracts;

namespace Comrade.Application.Components.Airplane.Commands;

public interface IAirplaneCommand
{
    Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
}
