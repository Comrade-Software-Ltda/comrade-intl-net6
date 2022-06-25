using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemMenuCore;

public interface ISystemMenuRepository : IRepository<SystemMenu>
{
    Task<ISingleResult<SystemMenu>> CodeUniqueValidation(Guid id, string text);
    IQueryable<SystemMenu> GetAllFathers();
}