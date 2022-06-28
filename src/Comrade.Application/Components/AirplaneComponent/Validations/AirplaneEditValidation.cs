using Comrade.Application.Components.AirplaneComponent.Contracts;

namespace Comrade.Application.Components.AirplaneComponent.Validations;

public class AirplaneEditValidation : AirplaneValidation<AirplaneEditDto>
{
    public AirplaneEditValidation()
    {
        ValidateCode();
        ValidateModel();
        ValidatePassengerQuantity();
    }
}