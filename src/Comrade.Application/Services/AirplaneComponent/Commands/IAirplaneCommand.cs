using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AirplaneComponent.Dtos;

namespace Comrade.Application.Services.AirplaneComponent.Commands;

public interface IAirplaneCommand
{
    Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
}