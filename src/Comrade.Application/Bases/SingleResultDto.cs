using Comrade.Application.Bases.Interfaces;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Enums;
using FluentValidation.Results;

namespace Comrade.Application.Bases;

public class SingleResultDto<TDto> : ResultDto, ISingleResultDto<TDto>
    where TDto : Dto
{
    public SingleResultDto(TDto? data)
    {
        Code = data == null ? (int) EnumResponse.NotFound : (int) EnumResponse.Ok;
        Success = data != null;
        Message = data == null
            ? BusinessMessage.MSG04
            : string.Empty;
        Data = data;
    }

    public SingleResultDto(SecurityResult errorSecurity)
    {
        Code = errorSecurity.Code;
        Success = false;
        Message = errorSecurity.ErrorMessage;
    }


    public SingleResultDto(Exception ex)
    {
        Code = (int) EnumResponse.InternalServerError;
        Success = false;
        Message = ex.Message;
        ExceptionMessage = ex.Message;
    }

    public SingleResultDto(IResult result)
    {
        Code = result.Code;
        Success = result.Success;
        Message = result.Message;
    }

    public SingleResultDto(List<ValidationFailure> failures)
    {
        Code = (int) EnumResponse.ErrorBusinessValidation;
        Success = false;
        Messages = failures.Select(x => x.ErrorMessage).ToList();
    }

    public ValidationResult? ValidationResult { get; }

    public TDto? Data { get; }
}
