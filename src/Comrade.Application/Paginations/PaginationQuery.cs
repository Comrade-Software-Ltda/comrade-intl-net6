namespace Comrade.Application.Paginations;

public class PaginationQuery(int pageNumber, int pageSize)
{
    public PaginationQuery() : this(1, 50)
    {
    }

    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
