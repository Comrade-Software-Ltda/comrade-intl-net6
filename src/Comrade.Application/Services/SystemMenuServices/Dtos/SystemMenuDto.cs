using System.ComponentModel.DataAnnotations;
using Comrade.Application.Bases;

namespace Comrade.Application.Services.SystemMenuServices.Dtos;

public class SystemMenuDto : EntityDto
{
    public SystemMenuDto? Father { get; set; }

    [Required(ErrorMessage = "Text is required")]
    public string? Text { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
    public string? Route { get; set; }
}