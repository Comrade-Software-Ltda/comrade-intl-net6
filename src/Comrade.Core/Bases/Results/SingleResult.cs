#region

using System.Collections.Generic;
using System.Globalization;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Messages;
using Comrade.Domain.Bases.Interfaces;
using Comrade.Domain.Enums;

#endregion

namespace Comrade.Core.Bases.Results
{
    public class SingleResult<TEntity> : ISingleResult<TEntity>
        where TEntity : IEntity
    {
        public SingleResult()
        {
            Code = (int) EnumResponse.Success;
            Success = true;
        }

        public SingleResult(string message)
        {
            Code = (int) EnumResponse.ErrorBusinessValidation;
            Success = false;
            Message = message;
        }

        public SingleResult(IEnumerable<string> messages)
        {
            Code = (int) EnumResponse.ErrorBusinessValidation;
            Success = false;
            Messages = messages;
        }


        public SingleResult(int code, string message)
        {
            Code = code;
            Success = false;
            Message = message;
        }

        public SingleResult(TEntity? data)
        {
            Code = data == null ? (int) EnumResponse.ErrorNotFound : (int) EnumResponse.Success;
            Success = data != null;
            Message = data == null
                ? BusinessMessage.ResourceManager.GetString("MSG04", CultureInfo.CurrentCulture)
                : string.Empty;
            Data = data;
        }

        public string? ExceptionMessage { get; set; }
        public IEnumerable<string>? Messages { get; set; }
        public int Code { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TEntity? Data { get; set; }
    }
}