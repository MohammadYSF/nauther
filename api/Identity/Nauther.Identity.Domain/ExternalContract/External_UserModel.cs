using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace Nauther.Identity.Domain.ExternalContract;

public abstract class External_UserModel
{
    public string Id { get; set; }
    public abstract  string GetUsername();
    public abstract string GetInfo();

    public abstract JObject GetJObject(); 
}

