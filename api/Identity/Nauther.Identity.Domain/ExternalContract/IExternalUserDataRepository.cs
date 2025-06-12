namespace Nauther.Identity.Domain.ExternalContract;

public interface IExternalUserDataRepository
{
    Task<ExternalUserModel> GetUserByUsernameAsync(string username);
    Task<ExternalUserModel> GetUserByIdentifierAsync(string id);
    Task<List<ExternalUserModel>> GetUsersAsync();
}