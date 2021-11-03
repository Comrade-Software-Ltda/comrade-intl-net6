using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Lookups;
using Comrade.Application.Paginations;
using Comrade.Application.Services.SystemUserServices.Dtos;

namespace Comrade.Application.Services.SystemUserServices.Queries;

public interface ISystemUserQuery
{
    Task<IPageResultDto<SystemUserDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemUserDto>> GetById(Guid id);
    Task<ListResultDto<LookupDto>> FindByName(string name);
}