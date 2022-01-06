using System.ComponentModel.DataAnnotations;
using Comrade.Api.Bases;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Paginations;
using Comrade.Application.Services.AirplaneComponent.Commands;
using Comrade.Application.Services.AirplaneComponent.Dtos;
using Comrade.Application.Services.AirplaneComponent.Queries;
using Microsoft.AspNetCore.Http;

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

    public AirplaneController(IAirplaneCommand airplaneCommand,
        IAirplaneQuery airplaneQuery, ILogger<AirplaneController> logger)
    {
        _airplaneCommand = airplaneCommand;
        _airplaneQuery = airplaneQuery;
        _logger = logger;
    }

    [HttpGet("get-all")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQuery? paginationQuery)
    {
        try
        {
            var result = await _airplaneQuery.GetAll(paginationQuery).ConfigureAwait(false);
            return StatusCode(result.Code, result);
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
    [HttpGet("get-by-id/{airplaneId:Guid}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
    public async Task<IActionResult> GetById([FromRoute] [Required] Guid airplaneId)
    {
        try
        {
            var result = await _airplaneQuery.GetByIdDefault(airplaneId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPost("create")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
    public async Task<IActionResult> Create([FromBody] [Required] AirplaneCreateDto dto)
    {
        try
        {
            var result = await _airplaneCommand.Create(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpPut("edit")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Edit))]
    public async Task<IActionResult> Edit([FromBody] [Required] AirplaneEditDto dto)
    {
        try
        {
            var result = await _airplaneCommand.Edit(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpDelete("delete/{airplaneId:Guid}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
    public async Task<IActionResult> Delete([FromRoute] [Required] Guid airplaneId)
    {
        try
        {
            var result = await _airplaneCommand.Delete(airplaneId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}