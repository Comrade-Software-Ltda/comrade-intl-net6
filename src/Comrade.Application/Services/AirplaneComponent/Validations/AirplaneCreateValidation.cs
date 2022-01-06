using Comrade.Application.Services.AirplaneComponent.Dtos;

namespace Comrade.Application.Services.AirplaneComponent.Validations;

public class AirplaneCreateValidation : AirplaneValidation<AirplaneCreateDto>
{
    public AirplaneCreateValidation()
    {
        ValidateCode();
        ValidateModel();
        ValidatePassengerQuantity();
    }
}