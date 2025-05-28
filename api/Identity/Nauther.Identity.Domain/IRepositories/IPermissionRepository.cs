using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IPermissionRepository : IBaseRepository<Permission>
{
    Task<int> GetCountAsync(string? search, CancellationToken cancellationToken);
    Task<IList<Permission>> GetAllListAsync(string search, PaginationListDto paginationListDto, CancellationToken cancellationToken);
    Task<IList<Permission>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task<IList<Permission>?> GetByNameAsync(string permissionName, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string permissionName, CancellationToken cancellationToken);
}