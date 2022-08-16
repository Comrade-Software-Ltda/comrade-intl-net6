using Comrade.Core.SystemRoleCore;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;

namespace Comrade.Persistence.Repositories;

public class SystemRoleRepository : Repository<SystemRole>, ISystemRoleRepository
{
    private readonly ComradeContext _context;

    public SystemRoleRepository(ComradeContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public IQueryable<Lookup>? FindByName(string name)
    {
        var result = _context.SystemRole
            .Where(x => x.Name.Contains(name)).Take(30)
            .OrderBy(x => x.Name)
            .Select(s => new Lookup { Key = s.Id, Value = s.Name });
        return result;
    }
}
