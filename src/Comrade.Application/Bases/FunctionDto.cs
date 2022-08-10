namespace Comrade.Application.Bases;

public class FunctionDto : Dto
{
    public FunctionDto(long n = -1, long fn = -1)
    {
        N  = n;
        Fn = fn;
    }

    public long N  { get; set; }
    public long Fn { get; set; }
}
