using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemRoleCore;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;
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
        var result = _context.SystemRoles
            .Where(x => x.Name.ToUpper().Trim().Contains(name.ToUpper().Trim())).Take(30)
            .OrderBy(x => x.Name.ToUpper().Trim())
            .Select(s => new Lookup {Key = s.Id, Value = s.Name.ToUpper().Trim()});
        return result;
    }

    public async Task<ISingleResult<SystemRole>> UniqueNameValidation(string name)
    {
        var exists = await _context.SystemRoles
            .Where(p => name.ToUpper().Trim()
                .Equals(p.Name.ToUpper().Trim()))
            .AnyAsync();

        return exists
            ? new SingleResult<SystemRole>((int) EnumResponse.ErrorBusinessValidation, BusinessMessage.MSG10)
            : new SingleResult<SystemRole>();
    }

    public async Task<ISingleResult<SystemRole>> UniqueTagValidation(string tag)
    {
        var exists = await _context.SystemRoles
            .Where(p => tag.ToUpper().Trim()
                .Equals(p.Tag.ToUpper().Trim(), StringComparison.Ordinal))
            .AnyAsync();

        return exists
            ? new SingleResult<SystemRole>((int) EnumResponse.ErrorBusinessValidation, BusinessMessage.MSG11)
            : new SingleResult<SystemRole>();
    }

    public async Task<SystemRole?> GetByIdIncludePermissions(Guid id)
    {
        return await _context.SystemRoles
            .Where(x => x.Id == id)
            .Include(x => x.SystemRolePermissions)
            .Include(x => x.SystemPermissions)
            .FirstOrDefaultAsync()
            ;
    }
}
