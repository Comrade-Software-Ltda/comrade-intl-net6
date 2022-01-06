using Comrade.Application.Bases;

namespace Comrade.Application.Services.SystemUserComponent.Dtos;

public class SystemUserDto : EntityDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Registration { get; set; }
    public DateTime? RegisterDate { get; set; }
}