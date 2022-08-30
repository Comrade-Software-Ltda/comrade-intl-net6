using System.ComponentModel.DataAnnotations;
using Comrade.Api.Bases;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Commands;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Queries;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.Application.Paginations;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1.SystemUserSystemRoleApi;

[FeatureGate(CustomFeature.SystemUserSystemRole)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class SystemUserSystemRoleController : ComradeController
{
    private readonly ISystemUserSystemRoleCommand _command;
    private readonly ISystemUserSystemRoleQuery _query;

    public SystemUserSystemRoleController(ISystemUserSystemRoleCommand command, ISystemUserSystemRoleQuery query)
    {
        _command = command;
        _query   = query;
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
            return StatusCode(StatusCodes.Status500InternalServerError,new SingleResultDto<EntityDto>(e));
        }
    }


    [HttpGet("get-by-id/{systemUserSystemRoleId:Guid}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
    public async Task<IActionResult> GetById([FromRoute][Required] Guid systemUserSystemRoleId)
    {
        try
        {
            var result = await _query.GetByIdDefault(systemUserSystemRoleId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPost("create")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
    public async Task<IActionResult> Create([FromBody][Required] SystemUserSystemRoleCreateDto dto)
    {
        try
        {
            var result = await _command.Create(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPut("edit")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Edit))]
    public async Task<IActionResult> Edit([FromBody][Required] SystemUserSystemRoleEditDto dto)
    {
        try
        {
            var result = await _command.Edit(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpDelete("delete/{systemUserSystemRoleId:Guid}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
    public async Task<IActionResult> Delete([FromRoute][Required] Guid systemUserSystemRoleId)
    {
        try
        {
            var result = await _command.Delete(systemUserSystemRoleId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,new SingleResultDto<EntityDto>(e));
        }
    }
}