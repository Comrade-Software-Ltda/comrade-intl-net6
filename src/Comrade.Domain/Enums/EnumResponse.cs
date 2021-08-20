namespace Comrade.Domain.Enums;

public enum EnumResponse
{
    None,
    Success = 200,
    ErrorBusinessValidation = 400,
    ErrorNotFound = 404,
    ErrorServer = 500
}
