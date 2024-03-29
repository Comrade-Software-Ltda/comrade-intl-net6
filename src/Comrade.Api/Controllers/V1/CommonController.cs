﻿using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUser.Queries;
using Comrade.Application.Lookups;
using Comrade.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Comrade.Api.Controllers.V1;

[FeatureGate(CustomFeature.Common)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CommonController(IServiceProvider serviceProvider, ISystemUserQuery systemUserQuery)
    : Controller
{
    [HttpGet("lookup-system-user")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetLookupSystemUser()
    {
        try
        {
            var service = serviceProvider.GetService<ILookupService<SystemUser>>()!;
            var result = await service.GetLookup();

            return Ok(new ListResultDto<LookupDto>(result));
        }
        catch (Exception e)
        {
            return Ok(new SingleResultDto<EntityDto>(e));
        }
    }


    [HttpGet("lookup-predicate-system-user-by-name/{name}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetLookupPredicateSystemUserByName(string name)
    {
        try
        {
            var service = serviceProvider.GetService<ILookupService<SystemUser>>()!;

            Expression<Func<SystemUser, bool>> expression = x => x.Name.Contains(name);
            var result = await service.GetLookup(expression);

            return Ok(new ListResultDto<LookupDto>(result));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpGet("lookup-system-user-by-name/{name}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetLookupSystemUserByName(string name)
    {
        try
        {
            var result = await systemUserQuery.FindByName(name);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}
