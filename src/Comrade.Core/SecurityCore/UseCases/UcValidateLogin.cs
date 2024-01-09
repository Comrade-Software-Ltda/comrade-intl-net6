using Comrade.Core.Bases.Results;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Core.SecurityCore.Validation;

namespace Comrade.Core.SecurityCore.UseCases;

public class UcValidateLogin(
    ISystemUserPasswordValidation systemUserPasswordValidation,
    IUcGenerateToken generateToken)
    : IUcValidateLogin
{
    public async Task<SecurityResult> Execute(Guid key, string password)
    {
        var result = await Task.Run(async () =>
        {
            var resultPassword = systemUserPasswordValidation.Execute(key, password);

            if (resultPassword.Success)
            {
                var selectedUser = resultPassword.Data!;

                var roles = new List<string> {"Role"};

                var user = new GenerateTokenCommand
                {
                    Id = key,
                    Name = selectedUser.Name,
                    Token = "",
                    Roles = roles
                };
                user.Token = await generateToken.Execute(user);

                return new SecurityResult(user);
            }

            return new SecurityResult(resultPassword.Code, resultPassword.Message);
        });

        return result;
    }
}
