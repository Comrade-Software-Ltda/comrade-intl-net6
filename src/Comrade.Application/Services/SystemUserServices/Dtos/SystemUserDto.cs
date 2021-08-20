#region

using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Services.SystemUserServices.Dtos;

public class SystemUserDto : EntityDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Registration { get; set; }
    public DateTime? RegisterDate { get; set; }
}
