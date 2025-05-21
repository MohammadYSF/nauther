using Microsoft.EntityFrameworkCore;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class BaseRepository<T>(AppDbContext context) : IBaseRepository<T>
    where T : class
{
    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<T>()
            .FindAsync(id, cancellationToken);
    }

    public async Task<T?> GetByIntIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Set<T>()
            .FindAsync(id, cancellationToken);
    }

    public virtual async Task<IList<T>?> GetAllListAsync(PaginationListDto paginationListDto, CancellationToken cancellationToken)
    {
        return await context.Set<T>()
            .AsNoTracking()
            .Skip((paginationListDto.PageNumber - 1) * paginationListDto.PageSize)
            .Take(paginationListDto.PageSize)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await context.Set<T>()
            .AddAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task<IList<T>> AddRangeAsync(IList<T> entities, CancellationToken cancellationToken)
    {
        await context.Set<T>().AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public virtual async Task RemoveAsync(T entity, CancellationToken cancellationToken)
    {
        context.Set<T>().Remove(entity);
    }

    public virtual async Task RemoveRange(IList<T> entities, CancellationToken cancellationToken)
    {
        context.Set<T>().RemoveRange(entities);
    }

    public Task SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        return await context.Set<T>().CountAsync();
    }
}