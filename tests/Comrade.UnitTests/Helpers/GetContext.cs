using Comrade.Persistence.DataAccess;

namespace Comrade.UnitTests.Helpers;

public static class GetContext
{
    public static ComradeContext? Execute(ServiceProvider sp)
    {
        var context = sp.GetService<ComradeContext>();
        return context;
    }
}