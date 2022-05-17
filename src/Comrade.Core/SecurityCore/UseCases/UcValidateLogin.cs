using Comrade.Core.Bases.Results;
using Comrade.Core.SecurityCore.Commands;
using Comrade.Core.SecurityCore.Validation;

namespace Comrade.Core.SecurityCore.UseCases;

public class UcValidateLogin : IUcValidateLogin
{
    private readonly IUcGenerateToken _generateToken;
    private readonly ISystemUserPasswordValidation _systemUserPasswordValidation;


    public UcValidateLogin(
        ISystemUserPasswordValidation systemUserPasswordValidation,
        IUcGenerateToken generateToken)
    {
        _systemUserPasswordValidation = systemUserPasswordValidation;
        _generateToken = generateToken;
    }

    public async Task<SecurityResult> Execute(Guid key, string password)
    {
        var result = await Task.Run(async () =>
        {
            var resultPassword = _systemUserPasswordValidation.Execute(key, password);

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
                user.Token = await _generateToken.Execute(user).ConfigureAwait(false);

                return new SecurityResult(user);
            }

            return new SecurityResult(resultPassword.Code, resultPassword.Message);
        }).ConfigureAwait(false);

        return result;
    }
}