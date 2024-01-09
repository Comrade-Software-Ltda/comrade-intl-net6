namespace Comrade.Application.Bases;

public class FunctionReturnDto(long n = -1, long fn = -1) : Dto
{
    public FunctionDto ResultDto { get; set; } = new(n, fn);
    public List<FunctionDto> DoCache { get; set; } = new(); // For tests only
    public List<FunctionDto> UseCache { get; set; } = new(); // For tests only
}
