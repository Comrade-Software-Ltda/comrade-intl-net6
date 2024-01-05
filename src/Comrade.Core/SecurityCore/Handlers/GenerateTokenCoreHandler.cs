using System.Threading;
using Comrade.Core.SecurityCore.Commands;
using MediatR;

namespace Comrade.Core.SecurityCore.Handlers;

public class
    GenerateTokenCoreHandler(IConfiguration configuration) : IRequestHandler<GenerateTokenCommand, string>
{
    public async Task<string> Handle(GenerateTokenCommand request,
        CancellationToken cancellationToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new("Key", request.Key.ToString()),
            new(ClaimTypes.Name, request.Name)
        };

        foreach (var role in request.Roles)
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
