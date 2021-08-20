#region

using Comrade.Application.Services.AirplaneServices.Dtos;

#endregion

namespace Comrade.Application.Services.AirplaneServices.Validations;

public class AirplaneEditValidation : AirplaneValidation<AirplaneEditDto>
{
    public AirplaneEditValidation()
    {
        ValidateId();
        ValidateCode();
        ValidateModel();
        ValidatePassengerQuantity();
    }
}
