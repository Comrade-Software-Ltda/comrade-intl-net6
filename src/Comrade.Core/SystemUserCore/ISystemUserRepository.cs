#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore;

public interface ISystemUserRepository : IRepository<SystemUser>
{
    IQueryable<Lookup>? FindByName(string name);
}
