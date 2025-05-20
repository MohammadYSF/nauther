namespace Nauther.Identity.Infrastructure.Utilities.PasswordHash;

public interface IPasswordHasherService
{
    string HashPassword(string password);
    bool VerifyPassword(string enteredPassword, string storedHash);
}