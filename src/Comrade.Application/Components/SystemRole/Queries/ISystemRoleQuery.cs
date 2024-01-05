using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemRole.Contracts;
using Comrade.Application.Lookups;
using Comrade.Application.Paginations;

namespace Comrade.Application.Components.SystemRole.Queries;

public interface ISystemRoleQuery
{
    Task<IPageResultDto<SystemRoleDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<IPageResultDto<SystemRoleWithPermissionsDto>> GetAllWithPermissions(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemRoleDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<SystemRoleDto>> GetByIdMongo(Guid id);
    Task<ListResultDto<LookupDto>> FindByName(string name);
}
