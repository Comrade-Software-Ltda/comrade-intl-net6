using Comrade.Application.Services.AirplaneComponent.Dtos;

namespace Comrade.Application.Services.AirplaneComponent.Validations;

public class AirplaneEditValidation : AirplaneValidation<AirplaneEditDto>
{
    public AirplaneEditValidation()
    {
        ValidateCode();
        ValidateModel();
        ValidatePassengerQuantity();
    }
}