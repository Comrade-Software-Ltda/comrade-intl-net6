using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Comrade.Application.Bases;

namespace Comrade.Application.Services.AirplaneComponent.Dtos;

public class AirplaneDto : EntityDto
{
    [DisplayName("Code")]
    [Required(ErrorMessage = "Please enter a Code")]
    public string? Code { get; set; }

    public string? Model { get; set; }
    public int PassengerQuantity { get; set; }
    public DateTime RegisterDate { get; set; }
}