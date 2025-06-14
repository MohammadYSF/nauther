using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class UserCredential
{
    public string UserId { get; set; }
    public string PasswordHash { get; set; }
}