using Comrade.Application.Bases;

namespace Comrade.Application.Components.AuthenticationComponent.Dtos;

public class UserDto : EntityDto
{
    public string? Token { get; set; }
}