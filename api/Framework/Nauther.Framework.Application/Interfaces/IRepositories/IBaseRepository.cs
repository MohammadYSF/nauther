using Nauther.Framework.Application.Common.DTOs;

namespace Nauther.Framework.Application.Interfaces.IRepositories;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<T?> GetByIntIdAsync(int id, CancellationToken cancellationToken);
    Task<IList<T>?> GetAllListAsync(PaginationListDto paginationListDto, CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task<IList<T>> AddRangeAsync(IList<T> entities, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task RemoveAsync(T entity, CancellationToken cancellationToken);
    Task RemoveRange(IList<T> entities, CancellationToken cancellationToken);
    Task SaveChangesAsync();
}