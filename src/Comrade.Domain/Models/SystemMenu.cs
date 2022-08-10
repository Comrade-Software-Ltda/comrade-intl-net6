using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;

[Table("syme_system_menu")]
public class SystemMenu : Entity
{
    [Column("syme_uuid_menu")]
    public Guid? MenuId { get; set; }
    public virtual SystemMenu? Menu { get; set; }
    public virtual List<SystemMenu>? Submenus { get; set; }

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