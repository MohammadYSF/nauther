using Nauther.Identity.Domain.ExternalContract;

namespace Nauther.Identity.Infrastructure.Services.ExternalUser;
using MongoDB.Driver;

public class External_AIED_UserDataRepository(IMongoDatabase mongoDatabase)
    : IExternalUserDataRepository<External_AIED_UserModel>
{
    private readonly IMongoCollection<External_AIED_UserModel> _userCollection =
        mongoDatabase.GetCollection<External_AIED_UserModel>("users");

    public async Task<External_AIED_UserModel?> GetUserByUsernameAsync(string username)
    {
        return await _userCollection
            .Find(user => user.Email == username)
            .FirstOrDefaultAsync();
    }

    public async Task<External_AIED_UserModel?> GetUserByIdentifierAsync(string id)
    {
        // I'm assuming the "id" is the document's "_id" or a unique identifier field
        return await _userCollection
            .Find(user => user.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<External_AIED_UserModel>> GetUsersAsync()
    {
        return await _userCollection.Find(FilterDefinition<External_AIED_UserModel>.Empty).ToListAsync();
    }
}
