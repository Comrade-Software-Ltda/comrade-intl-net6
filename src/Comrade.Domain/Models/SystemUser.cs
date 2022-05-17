using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("syus_system_user")]
public class SystemUser : Entity
{
    public SystemUser()
    {
        Name = "";
        Password = "";
        Registration = "";
    }

    public SystemUser(Guid id, string name, string email, string password, string registration,
        DateTime? registerDate)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Registration = registration;
        RegisterDate = registerDate;
    }

    [Column("syus_tx_name", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "NOME USU is required")]
    public string Name { get; set; } // varchar(255), not null

    [Column("syus_tx_email", TypeName = "varchar")]
    [MaxLength(255)]
    public string? Email { get; set; } // varchar(255), null

    [Column("syus_pw_password", TypeName = "varchar")]
    [MaxLength(1023)]
    public string Password { get; set; } // varchar(1023), not null

    [Column("syus_tx_registration", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Registration is required")]
    public string Registration { get; set; } // varchar(255), not null

    [Column("syus_dt_register", TypeName = "varchar")]
    public DateTime? RegisterDate { get; set; }
}