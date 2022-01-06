using Comrade.Application.Bases;

namespace Comrade.Application.Services.AuthenticationComponent.Dtos;

public class UserDto : EntityDto
{
    public string? Token { get; set; }
}