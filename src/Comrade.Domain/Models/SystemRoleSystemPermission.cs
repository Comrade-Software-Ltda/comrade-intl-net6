using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("syro_system_role_sype_system_permission")]
public class SystemRoleSystemPermission : Entity
{
    [Column("fk_uuid_system_role")] public Guid SystemRoleId { get; set; }

    public virtual SystemRole SystemRole { get; set; }

    [Column("fk_uuid_system_permission")] public Guid SystemPermissionId { get; set; }

    public virtual SystemPermission SystemPermission { get; set; }
}
