using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemMenuCore;

public interface ISystemMenuRepository : IRepository<SystemMenu>
{
    Task<ISingleResult<SystemMenu>> ValidateSameCode(Guid id, string code);
}