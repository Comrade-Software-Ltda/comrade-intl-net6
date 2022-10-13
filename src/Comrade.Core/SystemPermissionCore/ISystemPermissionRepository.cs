using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore;

public interface ISystemPermissionRepository : IRepository<SystemPermission>
{
    Task<ISingleResult<SystemPermission>> TagUniqueValidation(string tag);
}
