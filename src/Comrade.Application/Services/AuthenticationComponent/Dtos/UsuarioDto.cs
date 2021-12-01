using Comrade.Application.Bases;

namespace Comrade.Application.Services.AuthenticationServices.Dtos;

public class UserDto : EntityDto
{
    public string? Token { get; set; }
}