namespace Nauther.Identity.Domain.ExternalContract;

public interface IExternalUserDataRepository<T> where T :External_UserModel
{
    Task<T?> GetUserByUsernameAsync(string username);
    Task<T?> GetUserByIdentifierAsync(string id);
    Task<List<T>> GetUsersAsync();
}