namespace Comrade.Application.Bases;

public class AlticciDto : Dto
{
    public AlticciDto()
    {
        this.AlticciN = 0;
        this.AlticciAn = 0;
    }

    public AlticciDto(int? AlticciN, int? AlticciAn)
    {
        this.AlticciN = AlticciN;
        this.AlticciAn = AlticciAn;
    }

    public int? AlticciN { get; set; }
    public int? AlticciAn { get; set; }
}
