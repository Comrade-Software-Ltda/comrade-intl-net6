namespace Comrade.Core.Bases.Interfaces;

public interface IResult
{
    bool Success { get; set; }
    int Code { get; set; }
    string? Message { get; set; }
}
