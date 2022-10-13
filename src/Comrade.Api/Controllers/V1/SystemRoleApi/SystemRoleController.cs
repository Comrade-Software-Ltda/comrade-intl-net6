using System.ComponentModel.DataAnnotations;
using Comrade.Api.Bases;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Commands;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Application.Components.SystemRoleComponent.Queries;
using Comrade.Application.Paginations;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1.SystemRoleApi;

[FeatureGate(CustomFeature.SystemRole)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class SystemRoleController : ComradeController
{
    private readonly ISystemRoleCommand _command;
    private readonly ISystemRoleQuery _query;

    public SystemRoleController(ISystemRoleCommand command, ISystemRoleQuery query)
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


    [HttpGet("get-by-id/{systemRoleId:Guid}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
    public async Task<IActionResult> GetById([FromRoute] [Required] Guid systemRoleId)
    {
        try
        {
            var result = await _query.GetByIdDefault(systemRoleId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPost("create")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
    public async Task<IActionResult> Create([FromBody] [Required] SystemRoleCreateDto dto)
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
    public async Task<IActionResult> Edit([FromBody] [Required] SystemRoleEditDto dto)
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

    [HttpDelete("delete/{systemRoleId:Guid}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
    public async Task<IActionResult> Delete([FromRoute] [Required] Guid systemRoleId)
    {
        try
        {
            var result = await _command.Delete(systemRoleId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
        }
    }
}
