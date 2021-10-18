using AutoMapper;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Paginations;
using Comrade.Application.Services.SystemUserServices.Commands;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Application.Services.SystemUserServices.Queries;
using Microsoft.AspNetCore.Http;

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


    [HttpGet]
    [Route("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>),
        StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
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


    [HttpGet]
    [Route("get-by-id/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>),
        StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _systemUserQuery.GetById(id).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [Route("create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>),
        StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromBody] SystemUserCreateDto dto)
    {
        try
        {
            var result = await _systemUserCommand.Create(dto).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPut]
    [Route("edit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>),
        StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Edit([FromBody] SystemUserEditDto dto)
    {
        try
        {
            var result = await _systemUserCommand.Edit(dto).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>),
        StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _systemUserCommand.Delete(id).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}