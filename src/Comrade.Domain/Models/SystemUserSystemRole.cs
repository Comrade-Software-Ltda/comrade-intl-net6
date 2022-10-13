using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("syus_system_user_syro_system_role")]
public class SystemUserSystemRole : Entity
{
    [Column("fk_uuid_system_user")] public Guid SystemUserId { get; set; }

    public virtual SystemUser SystemUser { get; set; } = null!;

    [Column("fk_uuid_system_role")] public Guid SystemRoleId { get; set; }

    public virtual SystemRole SystemRole { get; set; } = null!;
}
