using Microsoft.EntityFrameworkCore;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;

    public override async Task<IList<User>?> GetAllListAsync(PaginationListDto paginationListDto, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Skip((paginationListDto.PageNumber - 1) * paginationListDto.PageSize)
            .Take(paginationListDto.PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserByPropertiesAsync(Guid? id, string? phoneNumber, 
        string? nationalCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}