using Comrade.Application.Components.Airplane.Contracts;

namespace Comrade.Application.Components.Airplane.Validations;

public class AirplaneEditValidation : AirplaneValidation<AirplaneEditDto>
{
    public AirplaneEditValidation()
    {
        ValidateCode();
        ValidateModel();
        ValidatePassengerQuantity();
    }
}
