namespace Comrade.Api.Bases;

public class ComradeController : ControllerBase
{
    [NonAction]
    protected int? GetUserId()
    {
        return User != null
            ? int.Parse(User.Claims.First(i => i.Type == "Key").Value,
                CultureInfo.CurrentCulture)
            : 0;
    }
}
