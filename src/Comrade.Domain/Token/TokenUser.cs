#region


#endregion

namespace Comrade.Domain.Token;

public class TokenUser
{
    public TokenUser(string key, string name, string token, IList<string> roles)
    {
        Key = key;
        Name = name;
        Token = token;
        Roles = roles;
    }

    public string Key { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }
    public IList<string> Roles { get; }
}
