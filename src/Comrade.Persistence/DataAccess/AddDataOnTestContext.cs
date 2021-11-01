using Comrade.Domain.Models;
using Comrade.Persistence.Extensions;

namespace Comrade.Persistence.DataAccess;

public static class AddDataOnTestContext
{
    private const string JsonPath = "Comrade.Persistence.SeedData";
    private static readonly object SyncLock = new();

    public static void Execute(ComradeContext? context)
    {
        var assembly = Assembly.GetAssembly(typeof(JsonUtilities));

        context.Database.EnsureDeleted();

        if (context != null)
        {
            return;
        }

        lock (SyncLock)
        {
            if (context != null && assembly is not null)
            {
                var airplanes =
                    JsonUtilities.GetListFromJson<Airplane>(
                        assembly.GetManifestResourceStream($"{JsonPath}.airplane.json"));
                var systemUser = JsonUtilities.GetListFromJson<SystemUser>(
                    assembly.GetManifestResourceStream($"{JsonPath}.system-user.json"));

                context.Airplanes.AddRange(airplanes!);
                context.SystemUsers.AddRange(systemUser!);

                if (context.Airplanes.Any())
                {
                    return;
                }

                context.SaveChanges();
            }
        }
    }
}