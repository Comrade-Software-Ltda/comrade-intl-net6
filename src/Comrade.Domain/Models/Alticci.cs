namespace Comrade.Domain.Models;

public class Alticci
{
    public Alticci()
    {
        this.AlticciN = 0;
        this.AlticciAn = 0;
    }

    public Alticci(int? AlticciN, int? AlticciAn)
    {
        this.AlticciN = AlticciN;
        this.AlticciAn = AlticciAn;
    }

    public int? AlticciN { get; set; }
    public int? AlticciAn { get; set; }
}
