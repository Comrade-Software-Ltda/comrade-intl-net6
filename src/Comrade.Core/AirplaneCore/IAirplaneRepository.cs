#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore;

public interface IAirplaneRepository : IRepository<Airplane>
{
    Task<ISingleResult<Airplane>> ValidateSameCode(int id, string code);
}
