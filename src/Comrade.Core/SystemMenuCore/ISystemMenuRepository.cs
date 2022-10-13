using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemMenuCore;

public interface ISystemMenuRepository : IRepository<SystemMenu>
{
    Task<ISingleResult<Entity>> UniqueValidation(SystemMenu entity);
    IQueryable<SystemMenu> GetAllMenus();
}
