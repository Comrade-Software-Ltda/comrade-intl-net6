using Comrade.Core.SystemMenuCore;
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
}