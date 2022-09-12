﻿using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("syro_system_role")]
public class SystemRole : Entity
{
    public SystemRole()
    {
        Name = "";
        SystemUsers = new HashSet<SystemUser>();
        SystemPermissions = new HashSet<SystemPermission>();
    }

    public SystemRole(string name)
    {
        Name = name;
        SystemUsers = new HashSet<SystemUser>();
        SystemPermissions = new HashSet<SystemPermission>();
    }
    
    [Column("syro_tx_name", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "NOME ROLE is required")]
    public string Name { get; set; } // varchar(255), not null

    public virtual ICollection<SystemUser> SystemUsers { get; set; }

    public virtual ICollection<SystemPermission> SystemPermissions { get; set; }
}