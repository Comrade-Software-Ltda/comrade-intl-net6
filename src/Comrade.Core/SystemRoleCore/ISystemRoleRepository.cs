using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore;

public interface ISystemRoleRepository : IRepository<SystemRole>
{
    IQueryable<Lookup>? FindByName(string name);
    Task<ISingleResult<SystemRole>> NameUniqueValidation(string name);
}
