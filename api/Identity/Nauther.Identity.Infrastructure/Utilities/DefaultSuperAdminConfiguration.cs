using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Infrastructure.Utilities;

public class DefaultSuperAdminConfiguration
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PasswordHash { get; set; }

    public User User
    {
        get
        {
            return new User() { Id = Id };
        }
    }
}