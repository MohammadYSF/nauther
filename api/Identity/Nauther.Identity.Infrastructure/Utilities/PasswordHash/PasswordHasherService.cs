using System.Text;
using Isopoh.Cryptography.Argon2;
using Konscious.Security.Cryptography;

namespace Nauther.Identity.Infrastructure.Utilities.PasswordHash;

public class PasswordHasherService : IPasswordHasherService
{

    public string HashPassword(string password)
    {
        var passwordHash = Isopoh.Cryptography.Argon2.Argon2.Hash(password);
        return passwordHash;
    }

    public bool VerifyPassword(string enteredPassword, string storedHash)
    {
        return Isopoh.Cryptography.Argon2.Argon2. Verify(storedHash, enteredPassword);
    }
}