using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemPermissionCore;
using Comrade.Domain.Enums;
using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;

namespace Comrade.Persistence.Repositories;

public class SystemPermissionRepository : Repository<SystemPermission>, ISystemPermissionRepository
{
    private readonly ComradeContext _context;

    public SystemPermissionRepository(ComradeContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ISingleResult<SystemPermission>> TagUniqueValidation(string tag)
    {
#pragma warning disable CA1304 // Specify CultureInfo
        var exists = await _context.SystemPermission
            .Where(p => tag.ToUpper().Trim()
                .Equals(p.Tag.ToUpper().Trim(), StringComparison.Ordinal))
            .AnyAsync().ConfigureAwait(false);
#pragma warning restore CA1304 // Specify CultureInfo
        return exists
            ? new SingleResult<SystemPermission>((int)EnumResponse.ErrorBusinessValidation, BusinessMessage.MSG11)
            : new SingleResult<SystemPermission>();
    }
}