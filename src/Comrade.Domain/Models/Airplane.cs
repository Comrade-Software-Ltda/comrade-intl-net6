using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("airp_airplane")]
public class Airplane : Entity
{
    public Airplane()
    {
        Code = "";
        Model = "";
        PassengerQuantity = 0;
    }

    public Airplane(string code, string model, int passengerQuantity, DateTime registerDate)
    {
        Code = code;
        Model = model;
        PassengerQuantity = passengerQuantity;
        RegisterDate = registerDate;
    }

    [Column("airp_tx_code", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Code is required")]
    public string Code { get; set; }

    [Column("airp_tx_model", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Model is required")]
    public string? Model { get; set; }

    [Column("airp_qt_passenger", TypeName = "int")]
    [Required(ErrorMessage = "PassengerQuantity is required")]
    public int PassengerQuantity { get; set; }

    [Column("airp_dt_register", TypeName = "varchar")]
    [Required(ErrorMessage = "RegisterDate is required")]
    public DateTime RegisterDate { get; set; }

    public override string Value => Code;
}