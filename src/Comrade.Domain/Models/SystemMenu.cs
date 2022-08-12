using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("syme_system_menu")]
public class SystemMenu : Entity
{
    [Column("syme_uuid_father")]
    public Guid? FatherId { get; set; }
    public virtual SystemMenu? Father { get; set; }
    public virtual List<SystemMenu>? Childrens { get; set; }

    [Column("syme_tx_text", TypeName = "varchar")]
    [MaxLength(30)]
    [Required(ErrorMessage = "Text is required")]
    public string? Text { get; set; }

    [Column("syme_tx_description", TypeName = "varchar")]
    [MaxLength(255)]
    [Required(ErrorMessage = "Description is required")]
    public string? Description { get; set; }

    [Column("syme_tx_route", TypeName = "varchar")]
    [MaxLength(255)]
    public string? Route { get; set; }
}