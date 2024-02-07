using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUser.Contracts;
using Comrade.Application.Lookups;
using Comrade.Application.Pagination;

namespace Comrade.Application.Components.SystemUser.Queries;

public interface ISystemUserQuery
{
    Task<IPageResultDto<SystemUserDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<ISingleResultDto<SystemUserDto>> GetByIdDefault(Guid id);
    Task<ISingleResultDto<SystemUserDto>> GetByIdMongo(Guid id);
    Task<ListResultDto<LookupDto>> FindByName(string name);

    Task<IPageResultDto<SystemUserWithPermissionsDto>> GetAllWithPermissions(
        PaginationQuery? paginationQuery = null);

    Task<IPageResultDto<SystemUserWithRolesDto>> GetAllWithRoles(
        PaginationQuery? paginationQuery = null);
}
