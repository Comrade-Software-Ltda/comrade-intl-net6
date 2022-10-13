using Comrade.Domain.Models;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Extensions;

namespace Comrade.UnitTests.DataInjectors;

public static class InjectDataOnContextBase
{
    private const string JsonPath = "Comrade.Persistence.SeedData";

    public static void InitializeDbForTests(ComradeContext db)
    {
        try
        {
            db.Database.EnsureDeleted();
            var assembly = Assembly.GetAssembly(typeof(JsonUtilities));
            if (assembly is not null)
            {
                var airplaneJson = assembly.GetManifestResourceStream($"{JsonPath}.airplane.json");
                var airplanes = JsonUtilities.GetListFromJson<Airplane>(airplaneJson);
                db.Airplanes.AddRange(airplanes!);

                var systemMenuJson = assembly.GetManifestResourceStream($"{JsonPath}.system-menu.json");
                var systemMenus = JsonUtilities.GetListFromJson<SystemMenu>(systemMenuJson);
                db.SystemMenus.AddRange(systemMenus!);

                var systemUserJson = assembly.GetManifestResourceStream($"{JsonPath}.system-user.json");
                var systemUsers = JsonUtilities.GetListFromJson<SystemUser>(systemUserJson);
                db.SystemUsers.AddRange(systemUsers!);

                var systemRoleJson = assembly.GetManifestResourceStream($"{JsonPath}.system-role.json");
                var systemRoles = JsonUtilities.GetListFromJson<SystemRole>(systemRoleJson);
                db.SystemRoles.AddRange(systemRoles!);

                var systemPermissionJson = assembly.GetManifestResourceStream($"{JsonPath}.system-permission.json");
                var systemPermissions = JsonUtilities.GetListFromJson<SystemPermission>(systemPermissionJson);
                db.SystemPermissions.AddRange(systemPermissions!);
            }

            db.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
