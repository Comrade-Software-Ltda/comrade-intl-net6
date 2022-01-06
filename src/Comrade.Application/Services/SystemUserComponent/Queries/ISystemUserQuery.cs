using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Lookups;
using Comrade.Application.Paginations;
using Comrade.Application.Services.SystemUserComponent.Dtos;

namespace Comrade.Application.Services.SystemUserComponent.Queries;

public interface ISystemUserQuery
{
    Task<IPageResultDto<SystemUserDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemUserDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<SystemUserDto>> GetByIdMongo(Guid id);
    Task<ListResultDto<LookupDto>> FindByName(string name);
}