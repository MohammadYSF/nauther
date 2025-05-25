using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories
{
    public interface IUserCredentialRepository : IBaseRepository<UserCredential>
    {
        Task<UserCredential> GetByUserIdAsync(string userId,CancellationToken cancellationToken);
    }
}
