namespace Comrade.Persistence.Extensions;

public static class QueryableExtension
{
    public static IQueryable<TEntity> OrderByCustom<TEntity>(this IQueryable<TEntity> source,
        string propertyName, bool descending)
    {
        string command = descending ? "OrderByDescending" : "OrderBy";
        var type = typeof(TEntity);
        var property = type.GetProperty(propertyName);
        var parameter = Expression.Parameter(type, "p");

        if (property is null)
        {
            return source;
        }

        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        var resultExpression = Expression.Call(typeof(Queryable), command,
            new Type[] { type, property.PropertyType },
            source.Expression, Expression.Quote(orderByExpression));

        return source.Provider.CreateQuery<TEntity>(resultExpression);
    }
}