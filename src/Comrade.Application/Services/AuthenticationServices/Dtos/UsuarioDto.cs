#region

using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Services.AuthenticationServices.Dtos;

public class UserDto : EntityDto
{
    public string? Token { get; set; }
}
