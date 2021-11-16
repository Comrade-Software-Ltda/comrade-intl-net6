using Comrade.Domain.Bases;

namespace Comrade.Domain.Models;
[Table("syme_system_menu")]
public class SystemMenu : Entity
{
    public SystemMenu()
    {
        Text = "";
    }
    public SystemMenu(Guid id, SystemMenu father, string text, string description, int order, string route)
    {
        Id = id;
        Father = father;
        Text = text;
        Description = description;
        Order = order;
        Route = route;
    }

    [ForeignKey("syme_uuid_father")]
    public SystemMenu? Father { get; set; }

    [Required(ErrorMessage = "Text is required")]
    [Column("syme_tx_text", TypeName = "varchar")]
    public string Text { get; set; }

    [Column("syme_tx_description", TypeName = "varchar")]
    public string? Description { get; set; }

    [Column("syme_nm_order", TypeName = "int")]
    public int? Order { get; set; }

    [Column("syme_tx_route", TypeName = "varchar")]
    public string? Route { get; set; }
}

