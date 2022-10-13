using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("syus_system_user_sype_system_permission")]
public class SystemUserSystemPermission : Entity
{
    [Column("fk_uuid_system_user")] public Guid SystemUserId { get; set; }

    public virtual SystemUser SystemUser { get; set; } = null!;

    [Column("fk_uuid_system_permission")] public Guid SystemPermissionId { get; set; }

    public virtual SystemPermission SystemPermission { get; set; } = null!;
}
