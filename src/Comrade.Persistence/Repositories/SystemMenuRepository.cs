using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemMenuCore;
using Comrade.Domain.Bases;
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

    public async Task<ISingleResult<Entity>> CodeUniqueValidation(Guid? menuId, string text)
    {
        var exists = await _context.SystemMenus
            .Where(p => p.MenuId == menuId && p.Text == text)
            .AnyAsync().ConfigureAwait(false);

        return exists
            ? new SingleResult<Entity>((int) EnumResponse.ErrorBusinessValidation,
                BusinessMessage.MSG20)
            : new SingleResult<Entity>();
    }
    
    public IQueryable<SystemMenu> GetAllMenus()
    {
        return _context.SystemMenus
            .Where(sm => sm.MenuId == null);
    }
    public override void Remove(Guid id)
    {
        var subMenus = _context.SystemMenus
            .Where(menu => menu.MenuId == id)
            .Select(menu => menu.Id)
            .ToList();

        base.RemoveAll(subMenus);
        base.Remove(id);
    }
}