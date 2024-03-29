﻿using Comrade.Application.Bases;

namespace Comrade.Application.Components.SystemMenu.Contracts;

public class SystemMenuSimpleDto : EntityDto
{
    public Guid? MenuId { get; set; }
    public string? Title { get; set; }
    public string? Icon { get; set; }
    public string? Description { get; set; }
    public string? Route { get; set; }
}
