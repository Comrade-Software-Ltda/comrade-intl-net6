using Comrade.Application.Components.AirplaneComponent.Contracts;

namespace Comrade.Application.Components.AirplaneComponent.Validations;

public class AirplaneCreateValidation : AirplaneValidation<AirplaneCreateDto>
{
    public AirplaneCreateValidation()
    {
        ValidateCode();
        ValidateModel();
        ValidatePassengerQuantity();
    }
}
