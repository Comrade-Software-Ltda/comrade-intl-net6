using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemMenuCore;
using Comrade.Domain.Enums;
using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;

namespace Comrade.Persistence.Repositories;

public class SystemMenuRepository : Repository<SystemMenu>, ISystemMenuRepository
{
    private readonly ComradeContext _context;

    public SystemMenuRepository(ComradeContext context)
        : base(context)
    {
        _context = context ??
                   throw new ArgumentNullException(nameof(context));
    }

    public async Task<ISingleResult<SystemMenu>> ValidateSameCode(Guid id, string text)
    {
        var exists = await _context.SystemMenus
            .Where(p => p.Id != id && text.Equals(p.Text, StringComparison.Ordinal))
            .AnyAsync().ConfigureAwait(false);

        return exists
            ? new SingleResult<SystemMenu>((int) EnumResponse.ErrorBusinessValidation,
                BusinessMessage.MSG08)
            : new SingleResult<SystemMenu>();
    }
}