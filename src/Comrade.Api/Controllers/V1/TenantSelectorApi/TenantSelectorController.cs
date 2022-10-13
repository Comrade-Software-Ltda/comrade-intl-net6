using System.ComponentModel.DataAnnotations;
using Comrade.Api.Bases;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Persistence.ADO;

namespace Comrade.Api.Controllers.V1.TenantSelectorApi;

[FeatureGate(CustomFeature.TenantSelector)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class TenantSelectorController : ComradeController
{
    private readonly CreateDatabase _createDatabase;
    private readonly GetAllDatabases _getAllDatabases;
    private readonly ILogger<TenantSelectorController> _logger;
    private readonly MigrateDatabase _migrateDatabase;

    public TenantSelectorController(ILogger<TenantSelectorController> logger, GetAllDatabases getAllDatabases,
        CreateDatabase createDatabase, MigrateDatabase migrateDatabase)
    {
        _logger = logger;
        _getAllDatabases = getAllDatabases;
        _createDatabase = createDatabase;
        _migrateDatabase = migrateDatabase;
    }


    [HttpGet("get-all-databases")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
    public IActionResult GetAllDatabases()
    {
        var teste = _getAllDatabases.Execute();
        return StatusCode(200, teste);
    }


    [HttpPost("create-database")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Post))]
    public IActionResult CreateDatabase([FromBody] [Required] string databaseName)
    {
        _createDatabase.Execute(databaseName);
        return StatusCode(201, null);
    }

    [HttpPost("migrate-database")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Post))]
    public async Task<IActionResult> MigrateDatabase([FromBody] [Required] string databaseName)
    {
        await _migrateDatabase.Execute();
        return StatusCode(201, null);
    }
}
