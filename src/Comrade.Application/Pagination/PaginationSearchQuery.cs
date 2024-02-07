namespace Comrade.Application.Pagination;

public class PaginationSearchQuery(int pageNumber, int pageSize, string propertyName, string searchValue)
{
    public PaginationSearchQuery() : this(1, 50,"","")
    {
    }

    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public string PropertyName { get; set; } = propertyName;
    public string SearchValue { get; set; } = searchValue;
}
