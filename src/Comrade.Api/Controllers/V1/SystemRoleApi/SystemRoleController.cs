using System.ComponentModel.DataAnnotations;
using Comrade.Api.Bases;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRole.Commands;
using Comrade.Application.Components.SystemRole.Contracts;
using Comrade.Application.Components.SystemRole.Queries;
using Comrade.Application.Paginations;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1.SystemRoleApi;

[FeatureGate(CustomFeature.SystemRole)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class SystemRoleController(ISystemRoleCommand command, ISystemRoleQuery query) : ComradeController
{
    [HttpGet("get-all")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQuery? paginationQuery)
    {
        try
        {
            var result = await query.GetAll(paginationQuery);
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
            var result = await query.GetByIdDefault(systemRoleId);
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
            var result = await command.Create(dto);
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
            var result = await command.Edit(dto);
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
            var result = await command.Delete(systemRoleId);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpGet("get-all-with-permissions")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetAllWithPermissions([FromQuery] PaginationQuery? paginationQuery)
    {
        try
        {
            var result = await query.GetAllWithPermissions(paginationQuery);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPut("manage-permissions")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Edit))]
    public async Task<IActionResult> ManagePermissions([FromBody] [Required] SystemRoleManagePermissionsDto dto)
    {
        try
        {
            var result = await command.ManagePermissions(dto);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}
