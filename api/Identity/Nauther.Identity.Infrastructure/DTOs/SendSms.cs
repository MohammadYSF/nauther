namespace Nauther.Identity.Infrastructure.DTOs;

public class SendSms
{
    public string Recipients { get; set; }
    public string Messages { get; set; }
    public int UserId { get; set; }
}