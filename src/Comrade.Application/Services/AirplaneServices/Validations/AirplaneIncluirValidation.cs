#region

using Comrade.Application.Services.AirplaneServices.Dtos;

#endregion

namespace Comrade.Application.Services.AirplaneServices.Validations;

public class AirplaneCreateValidation : AirplaneValidation<AirplaneCreateDto>
{
    public AirplaneCreateValidation()
    {
        ValidateCode();
        ValidateModel();
        ValidatePassengerQuantity();
    }
}
