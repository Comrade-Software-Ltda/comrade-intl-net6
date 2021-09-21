#region

using AutoMapper;
using Comrade.Api.Bases;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Paginations;
using Comrade.Application.Services.AirplaneServices.Commands;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Application.Services.AirplaneServices.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;

#endregion

namespace Comrade.Api.UseCases.V1.AirplaneApi;


// [Authorize]
[FeatureGate(CustomFeature.Airplane)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AirplaneController : ComradeController
{
    private readonly IAirplaneCommand _airplaneCommand;
    private readonly IAirplaneQuery _airplaneQuery;
    private readonly ILogger<AirplaneController> _logger;
    private readonly IMapper _mapper;

    public AirplaneController(IAirplaneCommand airplaneCommand,
        IAirplaneQuery airplaneQuery, IMapper mapper, ILogger<AirplaneController> logger)
    {
        _airplaneCommand = airplaneCommand;
        _airplaneQuery = airplaneQuery;
        _mapper = mapper;
        _logger = logger;
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

            var result = await _airplaneQuery.GetAll(paginationFilter).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    /// <summary>
    ///     get por id
    /// </summary>
    /// <param name="id"></param>
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
            var result = await _airplaneQuery.GetById(id).ConfigureAwait(false);
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
    public async Task<IActionResult> Create([FromBody] AirplaneCreateDto dto)
    {
        try
        {
            var result = await _airplaneCommand.Create(dto).ConfigureAwait(false);
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
    public async Task<IActionResult> Edit([FromBody] AirplaneEditDto dto)
    {
        try
        {
            var result = await _airplaneCommand.Edit(dto).ConfigureAwait(false);
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ISingleResultDto<EntityDto>))]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>),
        StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _airplaneCommand.Delete(id).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}
