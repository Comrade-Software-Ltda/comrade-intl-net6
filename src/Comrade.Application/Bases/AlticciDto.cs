namespace Comrade.Application.Bases;

public class AlticciDto : Dto
{
    public AlticciDto(long AlticciN, long AlticciAn)
    {
        this.AlticciN = AlticciN;
        this.AlticciAn = AlticciAn;
    }

    public long AlticciN { get; set; }
    public long AlticciAn { get; set; }
}
