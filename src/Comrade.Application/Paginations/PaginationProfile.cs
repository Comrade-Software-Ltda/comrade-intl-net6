#region

using AutoMapper;

#endregion

namespace Comrade.Application.Paginations;

public class PaginationProfile : Profile
{
    public PaginationProfile()
    {
        CreateMap<PaginationQuery, PaginationFilter>();
    }
}
