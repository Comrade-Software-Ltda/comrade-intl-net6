using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Application.Lookups;
using Comrade.Application.Paginations;

namespace Comrade.Application.Components.SystemUserComponent.Queries;

public interface ISystemUserQuery
{
    Task<IPageResultDto<SystemUserDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemUserDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<SystemUserDto>> GetByIdMongo(Guid id);
    Task<ListResultDto<LookupDto>> FindByName(string name);
}