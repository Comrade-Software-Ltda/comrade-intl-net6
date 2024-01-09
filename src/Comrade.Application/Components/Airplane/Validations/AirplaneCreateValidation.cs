using Comrade.Application.Components.Airplane.Contracts;

namespace Comrade.Application.Components.Airplane.Validations;

public class AirplaneCreateValidation : AirplaneValidation<AirplaneCreateDto>
{
    public AirplaneCreateValidation()
    {
        ValidateCode();
        ValidateModel();
        ValidatePassengerQuantity();
    }
}
