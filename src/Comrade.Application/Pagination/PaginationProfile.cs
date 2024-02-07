using AutoMapper;

namespace Comrade.Application.Pagination;

public class PaginationProfile : Profile
{
    public PaginationProfile()
    {
        CreateMap<PaginationQuery, PaginationFilter>();
    }
}
