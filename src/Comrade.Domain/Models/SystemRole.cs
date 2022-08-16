using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("syro_system_role")]
public class SystemRole : Entity
{
    public SystemRole()
    {
        Name = "";
    }

    public SystemRole(string name)
    {
        Name = name;
    }
    
    [Column("syro_tx_name", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "NOME ROLE is required")]
    public string Name { get; set; } // varchar(255), not null
}

