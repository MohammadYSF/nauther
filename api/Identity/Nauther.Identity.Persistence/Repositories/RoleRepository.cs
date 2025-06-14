using System.Collections;
using Microsoft.EntityFrameworkCore;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class RoleRepository(AppDbContext context) : BaseRepository<Role>(context), IRoleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IList<Role>> GetAllListAsync(string search, PaginationListDto paginationListDto,
        CancellationToken cancellationToken)
    {
        if (paginationListDto.Page > -1)
        {
            return await _context.Roles
                .AsNoTracking()
                .Where(a =>
                    (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)) ||
                    a.Name.ToLower().Contains(search.ToLower()) ||
                    a.DisplayName.ToLower().Contains(search.ToLower()))
                .Skip((paginationListDto.Page - 1) * paginationListDto.PageSize)
                .Take(paginationListDto.PageSize)
                .ToListAsync(cancellationToken);
        }
        else
        {
            return await _context.Roles
                .AsNoTracking()
                .Where(a =>
                    (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)) ||
                    a.Name.ToLower().Contains(search.ToLower()) ||
                    a.DisplayName.ToLower().Contains(search.ToLower()))
                .ToListAsync(cancellationToken);
        }

    }

    public async Task<IList<Role>?> GetByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .Where(r => r.Name.ToLower().Contains(roleName.ToLower()))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .AnyAsync(r => r.Name.ToLower() == roleName.ToLower(), cancellationToken);
    }

    public async Task<IList<Role>> GetByIds(List<Guid> roleIds, CancellationToken cancellationToken)
    {
        return await _context.Roles.AsNoTracking().Where(a => roleIds.Contains(a.Id)).ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountAsync(string? search, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .Where(a =>
                (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)) ||
                a.Name.ToLower().Contains(search.ToLower()) ||
                a.DisplayName.ToLower().Contains(search.ToLower()))
            .CountAsync(cancellationToken);
    }
}