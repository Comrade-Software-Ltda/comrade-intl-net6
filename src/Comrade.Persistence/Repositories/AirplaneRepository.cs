using Comrade.Application.Bases;
using Comrade.Application.Components.Airplane.Contracts;
using Comrade.Application.Pagination;
using Comrade.Application;
using Comrade.Core.AirplaneCore;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Enums;
using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;

namespace Comrade.Persistence.Repositories;

public class AirplaneRepository(ComradeContext context) : Repository<Airplane>(context), IAirplaneRepository
{
    private readonly ComradeContext _context = context ??
                                               throw new ArgumentNullException(nameof(context));

    public async Task<ISingleResult<Airplane>> CodeUniqueValidation(Guid id, string code)
    {
        var exists = await _context.Airplanes
            .Where(p => p.Id != id && code.Equals(p.Code))
            .AnyAsync();

        return exists
            ? new SingleResult<Airplane>((int) EnumResponse.ErrorBusinessValidation,
                BusinessMessage.MSG08)
        : new SingleResult<Airplane>();
    }

    public IQueryable<Airplane> GetByProjection(string propertyName, string searchValue)
    {
        var expression = ExpressionBuilder.BuildContainsExpression<Airplane>(propertyName, searchValue);
        var entity = this.GetByProjection(expression);

        return entity;
    }
}
