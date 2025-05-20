using System.Text;
using Konscious.Security.Cryptography;

namespace Nauther.Identity.Infrastructure.Utilities.PasswordHash;

public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password)
    { 
        using var hasher = new Argon2id(Encoding.UTF8.GetBytes(password));
        hasher.DegreeOfParallelism = 8;
        hasher.MemorySize = 65536; 
        hasher.Iterations = 4;
        byte[] hash = hasher.GetBytes(32);

        return Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string enteredPassword, string storedHash)
    {
        using var hasher = new Argon2id(Encoding.UTF8.GetBytes(enteredPassword));
        hasher.DegreeOfParallelism = 8;
        hasher.MemorySize = 65536;
        hasher.Iterations = 4;

        byte[] hash = hasher.GetBytes(32);
        return storedHash == Convert.ToBase64String(hash);
    }
}