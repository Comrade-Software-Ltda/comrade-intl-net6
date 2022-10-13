using Comrade.Domain.Bases;

namespace Comrade.Domain.Token;

public class TokenUser : Entity
{
    public string Name { get; set; }
    public string Token { get; set; }
    public List<string> Roles { get; set; }
}
