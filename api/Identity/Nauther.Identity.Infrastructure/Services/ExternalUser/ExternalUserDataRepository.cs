using EasyCaching.Core;
using Nauther.Identity.Domain.ExternalContract;
using Newtonsoft.Json;

namespace Nauther.Identity.Infrastructure.Services.ExternalUser;

public class ExternalUserDataRepository(IRedisCachingProvider easyCachingProvider)
    :IExternalUserDataRepository
{
    private readonly IRedisCachingProvider _easyCachingProvider = easyCachingProvider;
    private const string CACHE_KEY = "ids:userbasicinform";
    
    
    public async Task<ExternalUserModel> GetUserByUsernameAsync(string username)
    {
        var res = await this.GetUsersAsync();
        return res.FirstOrDefault(a => a.Username == username);
    }

    public async Task<ExternalUserModel> GetUserByIdentifierAsync(string id)
    {
        var user_in_cache = await _easyCachingProvider.HGetAsync(CACHE_KEY, id);
        return JsonConvert.DeserializeObject<ExternalUserModel>(user_in_cache);
    }

    public async Task<List<ExternalUserModel>> GetUsersAsync()
    {
        var res = await _easyCachingProvider.HGetAllAsync(CACHE_KEY);
        var x = res.Values.ToList();
        return x.Select(JsonConvert.DeserializeObject<ExternalUserModel>).ToList();
    }
}