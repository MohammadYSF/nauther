using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IGroupRepository : IBaseRepository<Group>
{
    Task<IList<Group>?> GetByNameAsync(string groupName, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string groupName, CancellationToken cancellationToken);
}