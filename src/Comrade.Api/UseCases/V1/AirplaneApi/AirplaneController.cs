using AutoMapper;
using Comrade.Api.Bases;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Paginations;
using Comrade.Application.Services.AirplaneServices.Commands;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Application.Services.AirplaneServices.Queries;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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
    ///     Get an airplane details.
    /// </summary>
    /// <param name="airplaneId"></param>
    [HttpGet("get-by-id/{airplaneId:int}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
    public async Task<IActionResult> GetById([FromRoute][Required] int airplaneId)
    {
        try
        {
            var result = await _airplaneQuery.GetById(airplaneId).ConfigureAwait(false);
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
    public async Task<IActionResult> Create([FromBody][Required] AirplaneCreateDto dto)
    {
        try
        {
            var result = await _airplaneCommand.Create(dto).ConfigureAwait(false);
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
    public async Task<IActionResult> Edit([FromBody][Required] AirplaneEditDto dto)
    {
        try
        {
            var result = await _airplaneCommand.Edit(dto).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status204NoContent, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpDelete("delete/{airplaneId:int}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
    public async Task<IActionResult> Delete([FromRoute][Required] int airplaneId)
    {
        try
        {
            var result = await _airplaneCommand.Delete(airplaneId).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}