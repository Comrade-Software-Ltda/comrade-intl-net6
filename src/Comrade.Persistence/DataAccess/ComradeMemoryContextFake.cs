using Comrade.Domain.Models;
using Comrade.Persistence.Extensions;

namespace Comrade.Persistence.DataAccess;

public static class ComradeMemoryContextFake
{
    private const string JsonPath = "Comrade.Persistence.SeedData";
    private static readonly object SyncLock = new();

    /// <summary>
    ///     To reset memory database use -> context.Database.EnsureDeleted().
    /// </summary>
    public static void AddDataFakeContext(ComradeContext? context)
    {
        var assembly = Assembly.GetAssembly(typeof(JsonUtilities));

        if (context != null && context.Airplanes.Any())
        {
            return;
        }

        lock (SyncLock)
        {
            if (context != null && assembly is not null)
            {
                var airplanes = JsonUtilities.GetListFromJson<Airplane>(
                    assembly.GetManifestResourceStream($"{JsonPath}.airplane.json"));
                var systemUser = JsonUtilities.GetListFromJson<SystemUser>(
                    assembly.GetManifestResourceStream($"{JsonPath}.system-user.json"));
                var systemMenu = JsonUtilities.GetListFromJson<SystemMenu>(
                    assembly.GetManifestResourceStream($"{JsonPath}.system-menu.json"));
                var systemRole = JsonUtilities.GetListFromJson<SystemRole>(
                    assembly.GetManifestResourceStream($"{JsonPath}.system-role.json"));
                var systemPermission = JsonUtilities.GetListFromJson<SystemPermission>(
                    assembly.GetManifestResourceStream($"{JsonPath}.system-permission.json"));

                context.Airplanes.AddRange(airplanes!);
                context.SystemUsers.AddRange(systemUser!);
                context.SystemMenus.AddRange(systemMenu!);
                context.SystemRole.AddRange(systemRole!);
                context.SystemPermission.AddRange(systemPermission!);

                if (context.Airplanes.Any())
                {
                    return;
                }

                context.SaveChanges();
            }
        }
    }
}