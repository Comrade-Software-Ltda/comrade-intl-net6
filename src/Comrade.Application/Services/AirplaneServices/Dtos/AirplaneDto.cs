using Comrade.Application.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Comrade.Application.Services.AirplaneServices.Dtos;

public class AirplaneDto : EntityDto
{
    [DisplayName("Code")]
    [Required(ErrorMessage = "Please enter a Code")]
    public string? Code { get; set; }

    public string? Model { get; set; }
    public int PassengerQuantity { get; set; }
    public DateTime RegisterDate { get; set; }
}