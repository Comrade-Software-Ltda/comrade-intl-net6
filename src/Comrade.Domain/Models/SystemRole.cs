using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("syro_system_role")]
public class SystemRole : Entity
{
    public SystemRole()
    {
        Name = "";
        Tag = "";
    }

    [Column("syro_tx_name", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Column("syro_tx_tag", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Tag is required")]
    public string Tag { get; set; }

    public virtual ICollection<SystemUser> SystemUsers { get; set; }
    public virtual ICollection<SystemUserSystemRole> SystemUserRoles { get; set; }
    public virtual ICollection<SystemPermission> SystemPermissions { get; set; }
    public virtual ICollection<SystemRoleSystemPermission> SystemRolePermissions { get; set; }
}
