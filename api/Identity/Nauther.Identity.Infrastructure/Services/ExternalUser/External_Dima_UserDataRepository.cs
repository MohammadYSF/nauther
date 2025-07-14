using EasyCaching.Core;
using Nauther.Identity.Domain.ExternalContract;
using Newtonsoft.Json;

namespace Nauther.Identity.Infrastructure.Services.ExternalUser;
using MongoDB.Driver;

public class External_Dima_UserDataRepository(IRedisCachingProvider easyCachingProvider)
    :IExternalUserDataRepository<External_Dima_UserModel>
{
    private readonly IRedisCachingProvider _easyCachingProvider = easyCachingProvider;
    private const string CACHE_KEY = "ids:userbasicinform";
    
    
    public async Task<External_Dima_UserModel?> GetUserByUsernameAsync(string username)
    {
        var res = await this.GetUsersAsync();
        return res.FirstOrDefault(a => a.Username == username);
    }

    public async Task<External_Dima_UserModel?> GetUserByIdentifierAsync(string id)
    {
        var user_in_cache = await _easyCachingProvider.HGetAsync(CACHE_KEY, id);
        return string.IsNullOrEmpty(user_in_cache)?null: JsonConvert.DeserializeObject<External_Dima_UserModel>(user_in_cache);
    }

    public async Task<List<External_Dima_UserModel>> GetUsersAsync()
    {
        var res = await _easyCachingProvider.HGetAllAsync(CACHE_KEY);
        var x = res.Values.ToList();
        return x.Select(JsonConvert.DeserializeObject<External_Dima_UserModel>).ToList();
    }
}