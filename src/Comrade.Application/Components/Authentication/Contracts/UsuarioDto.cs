using Comrade.Application.Bases;

namespace Comrade.Application.Components.Authentication.Contracts;

public class UserDto : EntityDto
{
    public string? Token { get; set; }
}
