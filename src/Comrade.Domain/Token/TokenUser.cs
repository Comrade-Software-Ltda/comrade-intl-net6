namespace Comrade.Domain.Token;

public class TokenUser
{
    public TokenUser(Guid key, string name, string token, IList<string> roles)
    {
        Key = key;
        Name = name;
        Token = token;
        Roles = roles;
    }

    public Guid Key { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }
    public IList<string> Roles { get; }
}