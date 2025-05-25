using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IPermissionRepository : IBaseRepository<Permission>
{
    Task<IList<Permission>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task<IList<Permission>?> GetByNameAsync(string permissionName, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string permissionName, CancellationToken cancellationToken);
}