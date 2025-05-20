namespace Nauther.Framework.Infrastructure.Common.DTOs;

public class GetUserDto
{
    public Guid? UserId { get; set; }
    public string? Username { get; set; }
    public string? PhoneNumber { get; set; }
    public List<string>? Permissions { get; set; }
}