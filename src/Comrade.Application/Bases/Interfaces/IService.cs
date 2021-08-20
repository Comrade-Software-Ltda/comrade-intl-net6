#region

using AutoMapper;

#endregion

namespace Comrade.Application.Bases.Interfaces;

public interface IService
{
    IMapper Mapper { get; }
}
