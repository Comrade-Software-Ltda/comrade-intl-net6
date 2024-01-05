namespace Comrade.Application.Bases;

public class FunctionDto(long n = -1, long fn = -1) : Dto
{
    public long N { get; set; } = n;
    public long Fn { get; set; } = fn;
}
