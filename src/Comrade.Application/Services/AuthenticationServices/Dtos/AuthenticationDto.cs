using Comrade.Application.Bases;

namespace Comrade.Application.Services.AuthenticationServices.Dtos;

public class AuthenticationDto : EntityDto
{
    public AuthenticationDto()
    {
        Key = "";
        Password = "";
    }

    public string Key { get; set; }
    public string Password { get; set; }
}