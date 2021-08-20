#region

using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Application.Bases;
using Comrade.Application.Lookups;
using Comrade.Application.Services.SystemUserServices.Queries;
using Comrade.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using System.Linq.Expressions;

#endregion

namespace Comrade.Api.UseCases.V1;

[FeatureGate(CustomFeature.Common)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CommonController : Controller
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISystemUserQuery _systemUserQuery;

    public CommonController(IServiceProvider serviceProvider, ISystemUserQuery systemUserQuery)
    {
        _serviceProvider = serviceProvider;
        _systemUserQuery = systemUserQuery;
    }


    [HttpGet]
    [Route("lookup-system-user")]
    public async Task<IActionResult> GetLookupSystemUser()
    {
        try
        {
            var service = _serviceProvider.GetService<ILookupService<SystemUser>>()!;

            var result = await service.GetLookup().ConfigureAwait(false);

            return Ok(new ListResultDto<LookupDto>(result));
        }
        catch (Exception e)
        {
            return Ok(new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpGet]
    [Route("lookup-predicate-system-user-by-name/{name}")]
    public async Task<IActionResult> GetLookupPredicateSystemUserByName(string name)
    {
        try
        {
            var service = _serviceProvider.GetService<ILookupService<SystemUser>>()!;

            Expression<Func<SystemUser, bool>> expression = x => x.Name.Contains(name);
            var result = await service.GetLookup(expression).ConfigureAwait(false);

            return Ok(new ListResultDto<LookupDto>(result));
        }
        catch (Exception e)
        {
            return Ok(new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpGet]
    [Route("lookup-system-user-by-name/{name}")]
    public async Task<IActionResult> GetLookupSystemUserByName(string name)
    {
        try
        {
            var result = await _systemUserQuery.FindByName(name).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return Ok(new SingleResultDto<EntityDto>(e));
        }
    }
}
