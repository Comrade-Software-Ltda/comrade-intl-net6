using Comrade.Application.Bases;

namespace Comrade.Application.Components.AuthenticationComponent.Contracts;

public class UserDto : EntityDto
{
    public string? Token { get; set; }
}
