namespace Comrade.Persistence;

public static class ExpressionBuilder
{
    public static Expression<Func<T, bool>> BuildContainsExpression<T>(string propertyName, string searchValue)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        var propertyInfo = typeof(T).GetProperty(propertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (propertyInfo == null)
        {
            throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T)}'.");
        }

        if (propertyInfo.PropertyType != typeof(string))
        {
            throw new ArgumentException($"Property '{propertyName}' is not a string type.");
        }

        var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
        var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
        var propertyAccessToLower = Expression.Call(propertyAccess, toLowerMethod);
        var containsMethod = typeof(string).GetMethod("Contains", new[] {typeof(string)});

        var someValue = Expression.Constant(searchValue.ToLower(), typeof(string));
        var containsExpression = Expression.Call(propertyAccessToLower, containsMethod, someValue);

        return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
    }
}
