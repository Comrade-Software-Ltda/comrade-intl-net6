#region

using System.Globalization;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;

#endregion

namespace Comrade.Core.Bases.Results
{
    public class DeleteResult<TEntity> : SingleResult<TEntity>
        where TEntity : Entity
    {
        public DeleteResult()
        {
            Code = (int) EnumResponse.Success;
            Success = true;
            Message = BusinessMessage.ResourceManager.GetString("MSG03",
                CultureInfo.CurrentCulture);
        }

        public DeleteResult(bool success, string? message)
        {
            Code = success ? (int) EnumResponse.Success : (int) EnumResponse.ErrorNotFound;
            Success = success;
            Message = message;
        }
    }
}