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

    public SystemPermission(string name, string tag)
    {
        Name = name;
        Tag = tag;
    }

    [Column("sype_tx_name", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "NOME permission is required")]
    public string Name { get; set; } // varchar(255), not null

    [Column("sype_tx_tag", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "TAG permission is required")]
    public string Tag { get; set; } // varchar(255), not null
}