using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemPermissionCore;
using Comrade.Domain.Enums;
using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;

namespace Comrade.Persistence.Repositories;

public class SystemPermissionRepository(ComradeContext context)
    : Repository<SystemPermission>(context), ISystemPermissionRepository
{
    private readonly ComradeContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<ISingleResult<SystemPermission>> TagUniqueValidation(string tag)
    {
        var exists = await _context.SystemPermissions
            .Where(p => tag.ToUpper().Trim()
                .Equals(p.Tag.ToUpper().Trim()))
            .AnyAsync();
        return exists
            ? new SingleResult<SystemPermission>((int) EnumResponse.ErrorBusinessValidation, BusinessMessage.MSG11)
            : new SingleResult<SystemPermission>();
    }
}
