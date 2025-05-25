using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;
using Polly;

namespace Nauther.Identity.Persistence.Repositories
{
    internal class UserCredentailRepository(AppDbContext context) : BaseRepository<UserCredential>(context), IUserCredentialRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<UserCredential> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(a => a.UserId == userId);
        }
    }
}
