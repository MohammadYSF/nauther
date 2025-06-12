namespace Nauther.Identity.Domain.ExternalContract;

public class ExternalUserModel
{
    public string Id { get; set; }
    public string UserCode { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public string ProfileImage { get; set; } = "https://example.com/images/user001.png";
}