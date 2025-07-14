using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace Nauther.Identity.Domain.ExternalContract;
[BsonIgnoreExtraElements]
public class External_AIED_UserModel : External_UserModel
{
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("first_name")]
    public string FirstName { get; set; }
    [BsonElement("last_name")]
    public string LastName { get; set; }
    [BsonElement("is_active")]
    public bool IsActive { get; set; }
    public override string GetUsername()
    {
        return Email;
    }
    public override JObject GetJObject()
    {
        var j = new JObject
        {
            ["id"] = Id,
            ["firstName"] = FirstName,
            ["email"] = Email,
            ["username"] = Email,
            ["lastName"] = LastName,
            ["isActive"] = IsActive
        };
        return j;
    }
    public override string GetInfo()
    {
        return new StringBuilder()
            .Append(Id)
            .Append(' ')
            .Append(Email)
            .Append(' ')
            .Append(FirstName)
            .Append(' ')
            .Append(LastName)
            .Append(' ')
            .ToString();
    }
}

