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

        if (context == null || assembly is null) return;
        if (context.Airplanes.Any()) return;

        lock (SyncLock)
        {
            var airplanes =
                JsonUtilities.GetListFromJson<Airplane>(
                    assembly.GetManifestResourceStream($"{JsonPath}.airplane.json"));

            airplanes?.ForEach(entity =>
            {
                var isRegistred = context.Airplanes.Any(x => x.Id == entity.Id);
                if (!isRegistred)
                    context.Airplanes.Add(entity);
            });

            var systemUsers = JsonUtilities.GetListFromJson<SystemUser>(
                assembly.GetManifestResourceStream($"{JsonPath}.system-user.json"));

            systemUsers?.ForEach(entity =>
            {
                var isRegistred = context.SystemUsers.Any(x => x.Id == entity.Id);
                if (!isRegistred)
                    context.SystemUsers.Add(entity);
            });
            var systemRoles = JsonUtilities.GetListFromJson<SystemRole>(
                assembly.GetManifestResourceStream($"{JsonPath}.system-role.json"));
            systemRoles?.ForEach(entity =>
            {
                var isRegistred = context.SystemRoles.Any(x => x.Id == entity.Id);
                if (!isRegistred)
                    context.SystemRoles.Add(entity);
            });
            var systemPermissions = JsonUtilities.GetListFromJson<SystemPermission>(
                assembly.GetManifestResourceStream($"{JsonPath}.system-permission.json"));
            systemPermissions?.ForEach(entity =>
            {
                var isRegistred = context.SystemPermissions.Any(x => x.Id == entity.Id);
                if (!isRegistred)
                    context.SystemPermissions.Add(entity);
            });
            var systemMenu = JsonUtilities.GetListFromJson<SystemMenu>(
                assembly.GetManifestResourceStream($"{JsonPath}.system-menu.json"));

            systemMenu?.ForEach(entity =>
            {
                var isRegistred = context.SystemMenus.Any(x => x.Id == entity.Id);
                if (!isRegistred)
                    context.SystemMenus.Add(entity);
            });

            context.SaveChanges();
        }
    }
}
