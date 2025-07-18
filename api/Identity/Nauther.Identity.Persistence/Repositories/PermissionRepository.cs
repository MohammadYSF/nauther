using Microsoft.EntityFrameworkCore;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class PermissionRepository(AppDbContext context) : BaseRepository<Permission>(context), IPermissionRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IList<Permission>?> GetByNameAsync(string permissionName, CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .AsNoTracking()
            .Where(p => p.Name.ToLower().Contains(permissionName.ToLower()))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string permissionName, CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .AsNoTracking()
            .AnyAsync(p => p.Name.ToLower() == permissionName.ToLower(), cancellationToken);
    }

    public async Task<IList<Permission>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _context.Permissions
       .AsNoTracking()
       .Where(a => ids.Contains(a.Id)).ToListAsync();
    }

    public async Task<IList<Permission>> GetAllListAsync(string search, PaginationListDto paginationListDto, CancellationToken cancellationToken)
    {
        if (paginationListDto.Page > -1)
        {
            return  await _context.Permissions
                .AsNoTracking()
                .Where(a =>
                    (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)) ||
                    a.Name.ToLower().Contains(search.ToLower()) ||
                    a.DisplayName.ToLower().Contains(search.ToLower())).Skip((paginationListDto.Page - 1) * paginationListDto.PageSize)
                .Take(paginationListDto.PageSize)
                .ToListAsync(cancellationToken);
        }
        else
        {
            return  await _context.Permissions
                .AsNoTracking()
                .Where(a =>
                    (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)) ||
                    a.Name.ToLower().Contains(search.ToLower()) ||
                    a.DisplayName.ToLower().Contains(search.ToLower()))
                .ToListAsync(cancellationToken);
        }

    }
    public async Task<int> GetCountAsync(string? search, CancellationToken cancellationToken)
    {
        
        return await _context.Permissions.AsNoTracking()
                  .Where(a =>
          (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)) ||
          a.Name.ToLower().Contains(search.ToLower()) ||
          a.DisplayName.ToLower().Contains(search.ToLower()))
                  .CountAsync(cancellationToken);
    }

}