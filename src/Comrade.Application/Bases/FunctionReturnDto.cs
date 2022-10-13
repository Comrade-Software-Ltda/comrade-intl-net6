namespace Comrade.Application.Bases;

public class FunctionReturnDto : Dto
{
    public FunctionReturnDto(long n = -1, long fn = -1)
    {
        ResultDto = new FunctionDto(n, fn);
        DoCache = new List<FunctionDto>();
        UseCache = new List<FunctionDto>();
    }

    public FunctionDto ResultDto { get; set; }
    public List<FunctionDto> DoCache { get; set; } // For tests only
    public List<FunctionDto> UseCache { get; set; } // For tests only
}
