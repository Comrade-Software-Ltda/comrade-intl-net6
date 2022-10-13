namespace Comrade.Domain.Enums;

public enum EnumResponse
{
    None,
    Ok = 200,
    Created = 201,
    Accepted = 202,
    NonAuthoritativeInformation = 203,
    NoContent = 204,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    ErrorBusinessValidation = 409,
    InternalServerError = 500
}
