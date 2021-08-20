#region

using Comrade.Domain.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Comrade.Domain.Models;


[Table("ussi_usuario_sistema")]
public class SystemUser : Entity
{
    public SystemUser()
    {
        Name = "";
        Password = "";
        Registration = "";
    }

    public SystemUser(int id, string name, string email, string password, string registration,
        DateTime? registerDate)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Registration = registration;
        RegisterDate = registerDate;
    }

    [Column("ussi_tx_nome", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "NOME USU is required")]
    public string Name { get; set; } // varchar(255), not null

    [Column("ussi_tx_email", TypeName = "varchar")]
    [MaxLength(255)]
    public string? Email { get; set; } // varchar(255), null

    [Column("ussi_pw_senha", TypeName = "varchar")]
    [MaxLength(1023)]
    [Required(ErrorMessage = "SENHA is required")]
    public string Password { get; set; } // varchar(1023), not null

    [Column("ussi_tx_matricula", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Registration is required")]
    public string Registration { get; set; } // varchar(255), not null

    [Column("ussi_dt_registro", TypeName = "varchar")]
    public DateTime? RegisterDate { get; set; }

    public override string Value => Name;
}
