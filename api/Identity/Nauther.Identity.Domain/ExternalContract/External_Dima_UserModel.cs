using System.Dynamic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Nauther.Identity.Domain.ExternalContract;
public class External_Dima_UserModel : External_UserModel
{
    public string UserCode { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public string ProfileImage { get; set; } = "https://example.com/images/user001.png";

    public override JObject GetJObject()
    {
        var j = new JObject
        {
            ["id"] = Id,
            ["userCode"] = UserCode,
            ["username"] = Username,
            ["phoneNumber"] = PhoneNumber,
            ["isActive"] = IsActive,
            ["profileImage"] = ProfileImage

        };
        return j;
    }
    public override string GetUsername()
    {
        return UserCode;
    }
    public override string GetInfo()
    {
        return new StringBuilder()
            .Append(UserCode)
            .Append(' ')
            .Append(Username).
            Append(' ')
            .Append(PhoneNumber)
            .ToString();
    }
}

