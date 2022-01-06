using Comrade.Core.AirplaneCore;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Enums;
using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;

namespace Comrade.Persistence.Repositories;

public class AirplaneRepository : Repository<Airplane>, IAirplaneRepository
{
    private readonly ComradeContext _context;

    public AirplaneRepository(ComradeContext context)
        : base(context)
    {
        _context = context ??
                   throw new ArgumentNullException(nameof(context));
    }

    public async Task<ISingleResult<Airplane>> CodeUniqueValidation(Guid id, string code)
    {
        var exists = await _context.Airplanes
            .Where(p => p.Id != id && code.Equals(p.Code, StringComparison.Ordinal))
            .AnyAsync().ConfigureAwait(false);

        return exists
            ? new SingleResult<Airplane>((int) EnumResponse.ErrorBusinessValidation,
                BusinessMessage.MSG08)
            : new SingleResult<Airplane>();
    }
}