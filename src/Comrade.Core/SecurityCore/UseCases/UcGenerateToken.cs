#region

using Comrade.Domain.Token;

#endregion

namespace Comrade.Core.SecurityCore.UseCases;


public class UcGenerateToken : IUcGenerateToken
{
    private readonly IConfiguration _configuration;

    public UcGenerateToken(
        IConfiguration configuration
    )
    {
        _configuration = configuration;
    }

    public string Execute(TokenUser tokenUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
                new("Key", tokenUser.Key),
                new(ClaimTypes.Name, tokenUser.Name)
            };

        foreach (var role in tokenUser.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
