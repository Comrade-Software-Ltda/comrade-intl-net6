using Comrade.Domain.Extensions;
using Comrade.Domain.Models;
using Comrade.Persistence.DataAccess;

namespace Comrade.UnitTests.DataInjectors;

public static class GetContextWithData
{
    public static ComradeContext Excute(ComradeContext context)
    {
        context.Airplanes.Add(new Airplane
        {
            Id = Guid.NewGuid(),
            Code = "Test",
            Model = "Test",
            PassengerQuantity = 666,
            RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia()
        });

        context.SystemRoles.Add(new SystemRole
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        });

        context.SaveChanges();

        return context;
    }
}
