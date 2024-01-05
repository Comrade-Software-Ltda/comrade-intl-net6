using Comrade.Core.SystemUserCore;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;

namespace Comrade.Persistence.Repositories;

public class SystemUserRepository(ComradeContext context) : Repository<SystemUser>(context), ISystemUserRepository
{
    private readonly ComradeContext _context = context ??
                                               throw new ArgumentNullException(nameof(context));


    public IQueryable<Lookup>? FindByName(string name)
    {
        var result = _context.SystemUsers
            .Where(x => x.Name.Contains(name)).Take(30)
            .OrderBy(x => x.Name)
            .Select(s => new Lookup {Key = s.Id, Value = s.Name});

        return result;
    }

    public async Task<SystemUser?> GetByIdIncludePermissions(Guid id)
    {
        return await _context.SystemUsers
                .Where(x => x.Id == id)
                .Include(x => x.SystemUserPermissions)
                .Include(x => x.SystemPermissions)
                .FirstOrDefaultAsync()
            ;
    }

    public async Task<SystemUser?> GetByIdIncludeRoles(Guid id)
    {
        return await _context.SystemUsers
                .Where(x => x.Id == id)
                .Include(x => x.SystemUserRoles)
                .Include(x => x.SystemRoles)
                .FirstOrDefaultAsync()
            ;
    }
}
