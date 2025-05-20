using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByPropertiesAsync(Guid? id, string? phoneNumber,
        string? nationalCode, CancellationToken cancellationToken);

    Task<User?> GetUserByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
}