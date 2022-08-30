using Comrade.Core.SystemUserSystemRoleCore;
using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;

namespace Comrade.Persistence.Repositories;

public class SystemUserSystemRoleRepository : Repository<SystemUserSystemRole>, ISystemUserSystemRoleRepository
{
    private readonly ComradeContext _context;

    public SystemUserSystemRoleRepository(ComradeContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}