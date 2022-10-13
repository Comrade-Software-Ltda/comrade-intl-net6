using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("sype_system_permission")]
public class SystemPermission : Entity
{
    public SystemPermission()
    {
        Name = "";
        Tag = "";
    }

    [Column("sype_tx_name", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Column("sype_tx_tag", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Tag is required")]
    public string Tag { get; set; }

    public virtual ICollection<SystemUser> SystemUsers { get; set; }
    public virtual ICollection<SystemUserSystemPermission> SystemUserPermissions { get; set; }
    public virtual ICollection<SystemRole> SystemRoles { get; set; }
    public virtual ICollection<SystemRoleSystemPermission> SystemRolePermissions { get; set; }
}
