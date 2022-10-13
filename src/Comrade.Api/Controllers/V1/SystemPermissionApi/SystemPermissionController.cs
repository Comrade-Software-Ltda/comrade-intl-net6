using System.ComponentModel.DataAnnotations;
using Comrade.Api.Bases;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Commands;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Application.Components.SystemPermissionComponent.Queries;
using Comrade.Application.Paginations;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1.SystemPermissionApi;

[FeatureGate(CustomFeature.SystemPermission)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class SystemPermissionController : ComradeController
{
    private readonly ISystemPermissionCommand _command;
    private readonly ISystemPermissionQuery _query;

    public SystemPermissionController(ISystemPermissionCommand command, ISystemPermissionQuery query)
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
            return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
        }
    }


    [HttpGet("get-by-id/{systemPermissionId:Guid}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
    public async Task<IActionResult> GetById([FromRoute] [Required] Guid systemPermissionId)
    {
        try
        {
            var result = await _query.GetByIdDefault(systemPermissionId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPost("create")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
    public async Task<IActionResult> Create([FromBody] [Required] SystemPermissionCreateDto dto)
    {
        try
        {
            var result = await _command.Create(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPut("edit")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Edit))]
    public async Task<IActionResult> Edit([FromBody] [Required] SystemPermissionEditDto dto)
    {
        try
        {
            var result = await _command.Edit(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpDelete("delete/{systemPermissionId:Guid}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
    public async Task<IActionResult> Delete([FromRoute] [Required] Guid systemPermissionId)
    {
        try
        {
            var result = await _command.Delete(systemPermissionId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
        }
    }
}
