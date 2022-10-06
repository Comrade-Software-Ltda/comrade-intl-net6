using System.ComponentModel.DataAnnotations;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemPermissionComponent.Commands;
using Comrade.Application.Components.SystemUserSystemPermissionComponent.Contracts;
using Comrade.Application.Components.SystemUserSystemPermissionComponent.Queries;
using Comrade.Application.Paginations;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1.SystemUserApi;

[FeatureGate(CustomFeature.SystemUserSystemPermission)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class SystemUserSystemPermissionController : ControllerBase
{
    private readonly ISystemUserSystemPermissionCommand _command;
    private readonly ISystemUserSystemPermissionQuery _query;

    public SystemUserSystemPermissionController(ISystemUserSystemPermissionCommand command,
        ISystemUserSystemPermissionQuery query)
    {
        _command = command;
        _query = query;
    }

    [HttpGet("get-all")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQuery? paginationQuery)
    {
        try
        {
            var result = await _query.GetAll(paginationQuery).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }


    [HttpPut("manage")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Edit))]
    public async Task<IActionResult> Edit([FromBody][Required] SystemUserSystemPermissionManageDto dto)
    {
        try
        {
            var result = await _command.Manage(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}
