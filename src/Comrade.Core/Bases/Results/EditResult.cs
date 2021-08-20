#region

using System.Globalization;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;

#endregion

namespace Comrade.Core.Bases.Results
{
    public class EditResult<TEntity> : SingleResult<TEntity>
        where TEntity : Entity
    {
        public EditResult()
        {
            Code = (int) EnumResponse.Success;
            Success = true;
            Message = BusinessMessage.ResourceManager.GetString("MSG02",
                CultureInfo.CurrentCulture);
        }

        public EditResult(bool success, string? message)
        {
            Code = success ? (int) EnumResponse.Success : (int) EnumResponse.ErrorNotFound;
            Success = success;
            Message = message;
        }
    }
}