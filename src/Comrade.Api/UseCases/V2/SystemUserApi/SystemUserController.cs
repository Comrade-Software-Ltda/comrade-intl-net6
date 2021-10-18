using AutoMapper;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Paginations;
using Comrade.Application.Services.SystemUserServices.Commands;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Application.Services.SystemUserServices.Queries;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Comrade.Api.UseCases.V2.SystemUserApi;

// [Authorize]
[FeatureGate(CustomFeature.SystemUser)]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class SystemUserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISystemUserCommand _systemUserCommand;
    private readonly ISystemUserQuery _systemUserQuery;

    public SystemUserController(ISystemUserCommand systemUserCommand,
        ISystemUserQuery systemUserQuery, IMapper mapper)
    {
        _systemUserCommand = systemUserCommand;
        _systemUserQuery = systemUserQuery;
        _mapper = mapper;
    }


    [HttpGet("get-all")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQuery? paginationQuery)
    {
        try
        {
            PaginationFilter? paginationFilter = null;
            if (paginationQuery != null)
            {
                paginationFilter =
                    _mapper.Map<PaginationQuery, PaginationFilter>(paginationQuery);
            }

            var result = await _systemUserQuery.GetAll(paginationFilter).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }


    [HttpGet("get-by-id/{systemUserId:int}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
    public async Task<IActionResult> GetById([FromRoute][Required] int systemUserId)
    {
        try
        {
            var result = await _systemUserQuery.GetById(systemUserId).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPost("create")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
    public async Task<IActionResult> Create([FromBody][Required] SystemUserCreateDto dto)
    {
        try
        {
            var result = await _systemUserCommand.Create(dto).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPut("edit")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Edit))]
    public async Task<IActionResult> Edit([FromBody][Required] SystemUserEditDto dto)
    {
        try
        {
            var result = await _systemUserCommand.Edit(dto).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status204NoContent, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpDelete("delete/{systemUserId:int}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
    public async Task<IActionResult> Delete([FromRoute][Required] int systemUserId)
    {
        try
        {
            var result = await _systemUserCommand.Delete(systemUserId).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}