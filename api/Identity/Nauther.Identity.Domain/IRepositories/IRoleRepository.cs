using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<IList<Role>> GetByIds(List<Guid> roleIds, CancellationToken cancellationToken);
    Task<IList<Role>?> GetByNameAsync(string roleName, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string roleName, CancellationToken cancellationToken);
}