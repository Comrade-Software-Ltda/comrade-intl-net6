using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AirplaneServices.Dtos;

namespace Comrade.Application.Services.AirplaneServices.Commands;

public interface IAirplaneCommand : IService
{
    Task<ISingleResultDto<AirplaneDto>> Create(AirplaneCreateDto dto);
    Task<ISingleResultDto<AirplaneDto>> Edit(AirplaneEditDto dto);
    Task<ISingleResultDto<AirplaneDto>> Delete(int id);
}