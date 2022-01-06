using Comrade.Application.Bases;

namespace Comrade.Application.Services.AuthenticationComponent.Dtos;

public class AuthenticationDto : EntityDto
{
    public AuthenticationDto()
    {
        Password = "";
    }

    public Guid Key { get; set; }
    public string Password { get; set; }
}