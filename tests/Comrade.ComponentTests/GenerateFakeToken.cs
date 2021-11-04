using Comrade.Core.SecurityCore.UseCases;
using Comrade.Domain.Token;

namespace Comrade.ComponentTests;

public static class GenerateFakeToken
{
    public static string Execute()
    {
        var myConfiguration = new Dictionary<string, string>
        {
            { "JWT:Key", "afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs" }
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();

        var ucGenerateToken = new UcGenerateToken(configuration);

        var roles = new List<string>
        {
            "Test"
        };

        var user = new TokenUser()
        {
            Id = new Guid(),
            Name = "Test",
            Token = "",
            Roles = roles
        };

        var token = ucGenerateToken.Execute(user);
        return token;
    }
}