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
#pragma warning disable CA1304 // Specify CultureInfo
        var result = _context.SystemRoles
            .Where(x => x.Name.ToUpper().Trim().Contains(name.ToUpper().Trim())).Take(30)
            .OrderBy(x => x.Name.ToUpper().Trim())
            .Select(s => new Lookup { Key = s.Id, Value = s.Name.ToUpper().Trim() });
#pragma warning restore CA1304 // Specify CultureInfo
        return result;
    }

    public async Task<ISingleResult<SystemRole>> NameUniqueValidation(string name)
    {
#pragma warning disable CA1304 // Specify CultureInfo
        var exists = await _context.SystemRoles
            .Where(p => name.ToUpper().Trim()
                .Equals(p.Name.ToUpper().Trim(), StringComparison.Ordinal))
            .AnyAsync().ConfigureAwait(false);
#pragma warning restore CA1304 // Specify CultureInfo
        return exists
            ? new SingleResult<SystemRole>((int)EnumResponse.ErrorBusinessValidation, BusinessMessage.MSG10)
            : new SingleResult<SystemRole>();
    }
}
