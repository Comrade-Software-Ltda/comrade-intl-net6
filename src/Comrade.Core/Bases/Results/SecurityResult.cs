#region

using Comrade.Domain.Enums;
using Comrade.Domain.Token;

#endregion

namespace Comrade.Core.Bases.Results
{
    public class SecurityResult
    {
        public SecurityResult(TokenUser tokenUser)
        {
            TokenUser = tokenUser;
            Code = (int) EnumResponse.Success;
            Success = true;
        }

        public SecurityResult(int code, string? message)
        {
            Code = code;
            ErrorMessage = message;
            Success = false;
        }

        public TokenUser? TokenUser { get; set; }
        public bool Success { get; set; }
        public int Code { get; set; }
        public string? ErrorMessage { get; set; }
    }
}