using AutoMapper;

namespace Comrade.Application.Bases.Interfaces;

public interface IService
{
    IMapper Mapper { get; }
}